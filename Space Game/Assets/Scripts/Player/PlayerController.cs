using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float topForwardSpeed = 100f;
    [SerializeField] private float topReverseSpeed = 20f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float inertialDamping = 0.9f;
    [SerializeField] private float velocity = 0;
    [SerializeField] private RectTransform crosshairTransform;
    public float Velocity 
    {
        get { return velocity; }
    }

    // Update is called once per frame
    void Update()
    {
        float pitch = crosshairTransform.position.y * 1f * Time.deltaTime;
        float yaw = crosshairTransform.position.x * 0f * Time.deltaTime;
        float roll = Input.GetAxis("Horizontal") * -100f * Time.deltaTime;

        Vector3 rotations = new Vector3(-pitch, yaw, roll);
        transform.Rotate(rotations);

        float z = Input.GetAxis("Vertical");

        if (z != 0)
        {
            velocity += z * acceleration * Time.deltaTime;
            velocity = Mathf.Clamp(velocity, -topReverseSpeed, topForwardSpeed);
        }
        else
        {
            velocity -= velocity * inertialDamping * Time.deltaTime;
            if (Mathf.Abs(velocity) < 0.01) velocity = 0f; 
        }

        transform.Translate(velocity * transform.forward * Time.deltaTime);
    }
}
