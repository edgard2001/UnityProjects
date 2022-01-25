using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public Rigidbody rb;

    [SerializeField] private Transform enemyTransform;

    [SerializeField] private float attackRange = 400, sightRange = 1000;
    //[SerializeField] private LayerMask playerMask;

    [SerializeField] private float chaseSpeed = 300;

    [SerializeField] private bool targetInSightRange, targetInAttackRange;
    private float attackCooldown;

    private Vector3 startPoint;
    private Vector3 patrolPoint;
    [SerializeField] private float patrolRange = 300, patrolSpeed = 50;
    private bool patrolPointSet;

    private Vector3 targetDisplacement;

    private Vector3 velocity;
    private float roll, pitch, yaw;

    public float lastDamageTaken, shieldRegenDelay = 3f, shieldRegenRate = 100;
    public int shield, maxShield = 500, health, maxHealth = 1000;
    public ScreenMarkers markersScript;

    private void Awake()
    {
        startPoint = transform.position;
        enemyTransform = GameObject.Find("Player").GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();

        health = maxHealth;
        shield = maxShield;
        lastDamageTaken = Time.time;

        markersScript = GameObject.Find("HUD").GetComponent<ScreenMarkers>();
        markersScript.AddEnemy(gameObject);
    }

    void FixedUpdate()
    {
        targetDisplacement = enemyTransform.position - transform.position;
        targetDisplacement = transform.InverseTransformDirection(targetDisplacement);
        float targetDistance = targetDisplacement.magnitude;

        if (targetDistance > sightRange) targetInSightRange = false;
        else targetInSightRange = true;

        if (targetDistance > attackRange) targetInAttackRange = false;
        else targetInAttackRange = true;

        if (!targetInSightRange && !targetInAttackRange) Patrol(); 
        if (targetInSightRange && !targetInAttackRange) Chase();
        if (targetInSightRange && targetInAttackRange) Attack();

        //regenerate shield
        if (Time.time - lastDamageTaken > shieldRegenDelay)
        {
            shield += Mathf.RoundToInt(shieldRegenRate * Time.deltaTime);
            shield = Mathf.Clamp(shield, 0, maxShield);
        }
    }

    void AimAtTarget(Vector3 targetPosition)
    {
        targetDisplacement = targetPosition - transform.position;
        targetDisplacement = transform.InverseTransformDirection(targetDisplacement);

        if (targetDisplacement.normalized.y - Vector3.forward.y < -0.001)
        {
            pitch = 300 * (Vector3.forward.y - targetDisplacement.normalized.y) / 1 * Time.deltaTime;
        }
        else if (targetDisplacement.normalized.y - Vector3.forward.y > 0.001)
        {
            pitch = -300 * (targetDisplacement.normalized.y - Vector3.forward.y) / 1 * Time.deltaTime;
        }

        if (targetDisplacement.normalized.x - Vector3.forward.x < -0.001)
        {
            yaw = -300 * (Vector3.forward.x - targetDisplacement.normalized.x) / 1 * Time.deltaTime;
        }
        else if (targetDisplacement.normalized.x - Vector3.forward.x > 0.001)
        {
            yaw = 300 * (targetDisplacement.normalized.x - Vector3.forward.x) / 1 * Time.deltaTime;
        }

        //Vector3 rotations = new Vector3(-pitch, yaw, roll);
        //transform.Rotate(rotations, Space.Self);

        Quaternion rot = Quaternion.AngleAxis(yaw, transform.up) * Quaternion.AngleAxis(pitch, transform.right) * Quaternion.AngleAxis(roll, transform.forward) * transform.rotation;
        rb.MoveRotation(rot);
    }

    void Patrol()
    {
        if (!patrolPointSet)
        {
            patrolPoint.x = startPoint.x + Random.Range(-patrolRange, patrolRange);
            patrolPoint.y = startPoint.y + Random.Range(-patrolRange, patrolRange);
            patrolPoint.z = startPoint.z + Random.Range(-patrolRange, patrolRange);
            patrolPointSet = true;
        }
        else
        {
            velocity.z = Mathf.Lerp(velocity.z, patrolSpeed, 10 * Time.deltaTime);
            AimAtTarget(patrolPoint);
            MoveToTarget();

            Vector3 displacement = patrolPoint - transform.position;
            float distance = displacement.magnitude;

            if (distance < 50)
            {
                patrolPointSet = false;
            }
        }
    }

    void Chase()
    {
        velocity.z = Mathf.Lerp(velocity.z, chaseSpeed, 10 * Time.deltaTime);
        AimAtTarget(enemyTransform.position);
        MoveToTarget();

    }

    void Attack()
    {
        velocity.z = Mathf.Lerp(velocity.z, 0, 100 * Time.deltaTime);
        AimAtTarget(enemyTransform.position);
        MoveToTarget();
        //RaycastHit hitInfo;
    }

    void Evade()
    {

    }

    void MoveToTarget()
    {
        Vector3 worldVelocity = velocity.x * transform.right + velocity.y * transform.up + velocity.z * transform.forward;
        rb.velocity = worldVelocity;
    }

    public void TakeDamage(int damage)
    {
        lastDamageTaken = Time.time;
        if (damage > 0)
        {
            if (shield > 0)
            {
                shield -= damage;
                if (shield < 0)
                {
                    health += shield;
                    shield = 0;
                    if (health < 0)
                    {
                        health = 0;
                        markersScript.RemoveEnemy(gameObject);
                        Destroy(gameObject);
                    }
                }
            }
            else
            {
                health -= damage;
                if (health < 0)
                {
                    health = 0;
                    markersScript.RemoveEnemy(gameObject);
                    Destroy(gameObject);
                }
            }
        }
    }
}
