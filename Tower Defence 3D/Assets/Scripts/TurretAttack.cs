using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAttack : MonoBehaviour
{
    [SerializeField] private Transform _turret;
    [SerializeField] public float Range { get; private set; } = 2.5f;
    [SerializeField] private float _roundsPerMinute = 600;
    [SerializeField] private float _damagePerBullet = 10;
    [SerializeField] private float _maxRotationSpeed = 0.01f;

    private List<Transform> _targets;
    private EnemyHealth _targetHealthSystem;
    private Coroutine _shootCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        _targets = new List<Transform>();
    }

    private void Update()
    {
        while (_targets.Count > 0 && _targets[0] == null)
        {
            _targets.Remove(_targets[0]);
        }
        if (_targets.Count == 0) return;

        Vector3 displacement = _targets[0].position - _turret.position;
        displacement.y = 0f;

        while (displacement.sqrMagnitude > Range * Range && _targets.Count > 1)
        {
            _targets.Remove(_targets[0]);
            if (_targets.Count == 0) return;

            displacement = _targets[0].position - _turret.position;
            displacement.y = 0f;
        }

        float angle = Vector3.SignedAngle(_turret.right, displacement, _turret.up);
        _turret.Rotate(_turret.up, angle);

        _targetHealthSystem = _targets[0].gameObject.GetComponent<EnemyHealth>();
        _targetHealthSystem.TakeDamage(_damagePerBullet * _roundsPerMinute / 60f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            _targets.Add(other.transform);
        }
    }

}

