using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    //movement
    [SerializeField] private float maxThrust = 300f, acceleration = 10f;
    private float currentThrust;
    private Vector3 velocity;

    //rotation
    [SerializeField] private bool invertVertivalInput;
    [SerializeField] private Vector2 rotationAmount;
    [SerializeField] private float rotationSpeedMultiplier = 200;
    private float maxFireFieldRadius = 0.1f, pitch, yaw, roll;

    //health and shield
    [SerializeField] private float shieldRegenDelay = 3f, shieldRegenRate = 100;
    [SerializeField] private int shield, maxShield = 500, health, maxHealth = 1000;
    private float lastDamageTaken;
    private Text healthText;

    //thruster rotate?
    private Transform leftThruster, rightThruster;

    //combat
    [SerializeField] private float maxFiringRange = 600;
    private Transform crosshairTransform;
    private Camera cam;
    private LineRenderer leftLaser, rightLaser;

    //particles
    [SerializeField] private GameObject impactParticle;
    private GameObject leftImpact, rightImpact;
    private ParticleSystem leftMainFlame, rightMainFlame;


    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        healthText.text = "Health: " + health.ToString();

        rb = GetComponent<Rigidbody>();
        rightThruster = this.transform.GetChild(0).GetChild(6).transform;
        leftThruster = this.transform.GetChild(0).GetChild(7).transform;

        crosshairTransform = GameObject.Find("Crosshair").GetComponent<Transform>();
        cam = GameObject.Find("Camera").GetComponent<Camera>();

        leftLaser = this.transform.GetChild(0).GetChild(9).GetComponent<LineRenderer>();
        leftLaser.SetPosition(0, leftLaser.transform.position);
        leftLaser.startWidth = 0.2f;
        leftLaser.endWidth = 0.2f;

        rightLaser = this.transform.GetChild(0).GetChild(10).GetComponent<LineRenderer>();
        rightLaser.SetPosition(0, rightLaser.transform.position);
        rightLaser.startWidth = 0.2f;
        rightLaser.endWidth = 0.2f;

        leftImpact = Instantiate(impactParticle, transform.position, transform.rotation);
        rightImpact = Instantiate(impactParticle, transform.position, transform.rotation);

        health = maxHealth;
        shield = maxShield;
        lastDamageTaken = -shieldRegenDelay;

        leftMainFlame = this.transform.GetChild(2).GetComponent<ParticleSystem>();
        rightMainFlame = this.transform.GetChild(3).GetComponent<ParticleSystem>();
    }

    void Update()
    {

        //keyaboard input
        rotationAmount.x = Input.mousePosition.x - Screen.width * 0.5f;
        rotationAmount.y = Screen.height * 0.5f - Input.mousePosition.y;

        rotationAmount.x = rotationAmount.x / (Screen.height * 0.5f);
        rotationAmount.y = rotationAmount.y / (Screen.height * 0.5f);

        rotationAmount = Vector3.ClampMagnitude(rotationAmount, 0.5f);

        //mouse input
        leftLaser.SetPosition(0, leftLaser.transform.position);
        rightLaser.SetPosition(0, rightLaser.transform.position);

        if (Input.GetMouseButton(0) && rotationAmount.magnitude < maxFireFieldRadius)
        {
            ShootMainGun();
        }
        else
        {
            leftImpact.SetActive(false);
            rightImpact.SetActive(false);
            leftLaser.SetPosition(1, leftLaser.transform.position);
            rightLaser.SetPosition(1, rightLaser.transform.position);
        }

        //regenerate shield
        if (Time.time - lastDamageTaken > shieldRegenDelay)
        {
            shield += Mathf.RoundToInt(shieldRegenRate * Time.deltaTime);
            shield = Mathf.Clamp(shield, 0, maxShield);
        }

        //set main thruster flame length
        leftMainFlame.startLifetime = 0.25f * currentThrust / maxThrust + 0.05f;
        rightMainFlame.startLifetime = 0.25f * currentThrust / maxThrust + 0.05f;
    }

    void FixedUpdate()
    {

        pitch = rotationAmount.y * rotationSpeedMultiplier * 2 * (invertVertivalInput ? -1 : 1) * Time.deltaTime;
        yaw = rotationAmount.x * rotationSpeedMultiplier * 2 * Time.deltaTime;
        roll = Input.GetAxis("Roll") * rotationSpeedMultiplier * Time.deltaTime;

        if (!Input.GetMouseButton(1))
        {
            Quaternion rot = Quaternion.AngleAxis(yaw, transform.up) * Quaternion.AngleAxis(pitch, transform.right) * Quaternion.AngleAxis(roll, transform.forward) * transform.rotation;
            rb.MoveRotation(rot);
        }
        else
        {
            Quaternion rot = Quaternion.AngleAxis(roll, transform.forward) * transform.rotation;
            rb.MoveRotation(rot);
        }

        currentThrust += Input.GetAxis("Vertical") * 300 * Time.deltaTime;
        currentThrust = Mathf.Clamp(currentThrust, 0, maxThrust);

        velocity.y = Mathf.Lerp(velocity.y, Input.GetAxis("Hover") * 30, 10 * Time.deltaTime);
        velocity.z = Mathf.Lerp(velocity.z, currentThrust, acceleration * Time.deltaTime);

        if (Physics.Raycast(transform.position, transform.forward, 11))
        {
            currentThrust = 0f;
            velocity.z = 0f;
        }

        Vector3 worldVelocity = velocity.x * transform.right + velocity.y * transform.up + velocity.z * transform.forward;
        rb.velocity = worldVelocity;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "meteor")
        {
            health -= 10;
            healthText.text = "Health: " + health.ToString();
            //Debug.Log("Hit");
        }
    }

    private void ShootMainGun()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.ScreenPointToRay(crosshairTransform.position), out hit, maxFiringRange))
        {
            if (hit.collider.tag != "Player")
            {
                if (hit.collider.tag == "enemy")
                {
                    //EnemyController target = hit.transform.GetComponent<EnemyController>();
                    EnemyController targetScript = hit.collider.GetComponentInParent<EnemyController>();
                    targetScript.TakeDamage(Mathf.RoundToInt(500 * Time.deltaTime));
                }
                leftImpact.SetActive(true);
                leftImpact.transform.position = hit.point;
                leftImpact.transform.rotation = Quaternion.LookRotation(hit.normal);
                leftLaser.SetPosition(1, hit.point);
                rightImpact.SetActive(true);
                rightImpact.transform.position = hit.point;
                rightImpact.transform.rotation = Quaternion.LookRotation(hit.normal);
                rightLaser.SetPosition(1, hit.point);
                return;
            }
        }

        RaycastHit hitLeft;
        Vector3 dirLeft = cam.ScreenToWorldPoint(crosshairTransform.position) + cam.transform.forward * maxFiringRange - leftLaser.transform.position;
        if (Physics.Raycast(leftLaser.transform.position, dirLeft.normalized, out hitLeft, maxFiringRange))
        {
            if (hitLeft.collider.tag == "enemy")
            {
                //EnemyController target = hit.transform.GetComponent<EnemyController>();
                EnemyController targetScript = hitLeft.collider.GetComponentInParent<EnemyController>();
                targetScript.TakeDamage(Mathf.RoundToInt(250 * Time.deltaTime));
            }
            leftImpact.SetActive(true);
            leftImpact.transform.position = hitLeft.point;
            leftImpact.transform.rotation = Quaternion.LookRotation(hitLeft.normal);
            leftLaser.SetPosition(1, hitLeft.point);
        }
        else
        {
            leftImpact.SetActive(false);
            leftLaser.SetPosition(1, cam.ScreenToWorldPoint(crosshairTransform.position) + cam.ScreenPointToRay(crosshairTransform.position).direction * maxFiringRange);
        }

        RaycastHit hitRight;
        Vector3 dirRight = cam.ScreenToWorldPoint(crosshairTransform.position) + cam.transform.forward * maxFiringRange - leftLaser.transform.position;
        if (Physics.Raycast(rightLaser.transform.position, dirRight.normalized, out hitRight, maxFiringRange))
        {
            if (hitRight.collider.tag == "enemy")
            {
                //EnemyController target = hit.transform.GetComponent<EnemyController>();
                EnemyController targetScript = hitRight.collider.GetComponentInParent<EnemyController>();
                targetScript.TakeDamage(Mathf.RoundToInt(250 * Time.deltaTime));
            }
            rightImpact.SetActive(true);
            rightImpact.transform.position = hitRight.point;
            rightImpact.transform.rotation = Quaternion.LookRotation(hitRight.normal);
            rightLaser.SetPosition(1, hitRight.point);
        }
        else
        {
            rightImpact.SetActive(false);
            rightLaser.SetPosition(1, cam.ScreenToWorldPoint(crosshairTransform.position) + cam.ScreenPointToRay(crosshairTransform.position).direction * maxFiringRange);
        }

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
                    }
                }
            }
            else
            {
                health -= damage;
                if (health < 0)
                {
                    health = 0;
                }
            }
        }
    }

}
