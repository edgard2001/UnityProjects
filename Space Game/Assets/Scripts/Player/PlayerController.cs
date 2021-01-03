using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Vector3 velocity;

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

        velocity.x = Mathf.Lerp(velocity.x, Input.GetAxis("Horizontal") * 30, 10 * Time.deltaTime);
        velocity.y = Mathf.Lerp(velocity.y, Input.GetAxis("Hover") * 30, 10 * Time.deltaTime);
        velocity.z = Mathf.Lerp(velocity.z, Input.GetAxis("Vertical") * 100, 10 * Time.deltaTime);

        transform.Translate(velocity.x * Vector3.right * Time.deltaTime);
        transform.Translate(velocity.y * Vector3.up * Time.deltaTime);
        transform.Translate(velocity.z * Vector3.forward * Time.deltaTime);

    }

}
