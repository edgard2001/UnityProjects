using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;

    [SerializeField] private float maxThrust = 300f;
    [SerializeField] private float acceleration = 10f;
    private float currentThrust;

    private Vector3 velocity;
    private float pitch, yaw, roll;

    //health stuff
    [SerializeField]  private int health = 100;
    public Text healthText;

    //thruster rotate?
    public Transform leftThruster;
    public Transform rightThruster;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        healthText = GameObject.Find("HealthText").GetComponent<Text>();
        healthText.text = "Health: " + health.ToString();

        rb = GetComponent<Rigidbody>();
        rightThruster = this.transform.GetChild(0).GetChild(6).transform;
        leftThruster = this.transform.GetChild(0).GetChild(7).transform;
    }

    private void Update()
    {
        
    }


    void FixedUpdate()
    {
        pitch = (Input.mousePosition.y - Screen.height * 0.5f) * -1f * Time.deltaTime;
        yaw = (Input.mousePosition.x - Screen.width * 0.5f) * 1f * Time.deltaTime;
        roll = Input.GetAxis("Roll") * 200f * Time.deltaTime;

        if (!Input.GetMouseButton(1))
        {
            Quaternion rot = Quaternion.AngleAxis(yaw, transform.up) * Quaternion.AngleAxis(pitch, transform.right) * Quaternion.AngleAxis(roll, transform.forward) * transform.rotation;
            rb.MoveRotation(rot);
        }

        currentThrust += Input.GetAxis("Vertical") * 300 * Time.deltaTime;
        velocity.y = Mathf.Lerp(velocity.y, Input.GetAxis("Hover") * 30, 10 * Time.deltaTime);

        currentThrust = Mathf.Clamp(currentThrust, 0, maxThrust);
        velocity.z = Mathf.Lerp(velocity.z, currentThrust, acceleration * Time.deltaTime);

        Vector3 worldVelocity = velocity.x * transform.right + velocity.y * transform.up + velocity.z * transform.forward;
        rb.velocity = worldVelocity;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "meteor")
        {
            health -= 10;
            healthText.text = "Health: " + health.ToString();
            Debug.Log("Hit");
            velocity = Vector3.zero;
        }
    }

}
