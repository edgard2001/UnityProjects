using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobMovement : MonoBehaviour
{
    [SerializeField] private TileNavigation _navigation;
    [SerializeField] private Vector3 _endNode;
    [SerializeField] private float _speed = 1f;

    private List<Vector3> _path;
    private Vector3 _nextNodePosition;
    private MobHealth _health;

    // Start is called before the first frame update
    void Start()
    {
        _navigation ??= GameObject.Find("NavigationSystem").GetComponent<TileNavigation>();
        _endNode = GameObject.Find("End").GetComponent<Transform>().position;

        _path = _navigation.GetPath(gameObject.transform.position, _endNode);
        if (_path.Count > 0) _nextNodePosition = _path[0];
        else _nextNodePosition = gameObject.transform.position;

        _health = gameObject.GetComponent<MobHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_path.Count == 0) return;
        
        gameObject.transform.position += (_nextNodePosition - gameObject.transform.position).normalized * _speed * Time.deltaTime;
        if ((gameObject.transform.position - _nextNodePosition).sqrMagnitude < 0.01f)
        {
            if (_path.Count > 0)
            {
                _path.Remove(_path[0]);
                _nextNodePosition = _path[0];
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.tag)
        {
            case "Finish":
                // Remove lives
                Destroy(gameObject);
                break;
            case "Trap":
                _health.TakeDamage(20f);
                break;
        }
    }
}
