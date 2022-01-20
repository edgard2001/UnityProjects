using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private EnemySpawner _spawnSystem;
    [SerializeField] private float _speed = 10f;
    private Vector3 _currentWaypoint;


    // Start is called before the first frame update
    void Start()
    {
        _spawnSystem = GameObject.FindWithTag("EnemySpawner").GetComponent<EnemySpawner>();
        if (_spawnSystem == null) Destroy(gameObject);
        _currentWaypoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if ((transform.position - _currentWaypoint).sqrMagnitude < 0.001f)
        {
            UpdateWaypoint();
            transform.LookAt(_currentWaypoint, transform.up);
        }

        Vector3 displacement = (_currentWaypoint - transform.position).normalized * _speed * Time.deltaTime;
        if (displacement.sqrMagnitude > (_currentWaypoint - transform.position).sqrMagnitude)
        {
            transform.position = _currentWaypoint;
        }
        else
        {
            transform.position += displacement;
        }
    }

    private void UpdateWaypoint()
    {
        _currentWaypoint = _spawnSystem.GetNextWaypoint(transform.position);
        if ((transform.position - _currentWaypoint).sqrMagnitude < 0.001f)
        {
            // Remove Lives
            Destroy(gameObject);
        }
    }

}
