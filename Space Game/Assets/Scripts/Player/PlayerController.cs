using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private float maxThrust = 300f;
    [SerializeField] private float acceleration = 10f;
    private float currentThrust;
    private Vector3 velocity;

    void FixedUpdate()
    {

        float pitch = (Input.mousePosition.y - Screen.height * 0.5f) * 1f * Time.deltaTime;
        float yaw = (Input.mousePosition.x - Screen.width * 0.5f) * 1f * Time.deltaTime;
        float roll = Input.GetAxis("Roll") * 200f * Time.deltaTime;

        if (!Input.GetMouseButton(1))
        {
            Vector3 rotations = new Vector3(-pitch, yaw, roll);
            transform.Rotate(rotations, Space.Self);
        }

        currentThrust += Input.GetAxis("Vertical") * 300 * Time.deltaTime;
        currentThrust = Mathf.Clamp(currentThrust, 0, maxThrust);

        velocity.y = Mathf.Lerp(velocity.y, Input.GetAxis("Hover") * 30, 10 * Time.deltaTime);
        velocity.z = Mathf.Lerp(velocity.z, currentThrust, acceleration * Time.deltaTime);

        transform.Translate(velocity.y * Vector3.up * Time.deltaTime);
        transform.Translate(velocity.z * Vector3.forward * Time.deltaTime);

    }

}
