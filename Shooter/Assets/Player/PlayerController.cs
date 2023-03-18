using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform headTransform;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float sensitivity = 1f;

    private float xAngle;

    void Start()
    {

        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Vector3 velocity = transform.right * Input.GetAxisRaw("Horizontal") + transform.forward * Input.GetAxisRaw("Vertical");

        rb.velocity = velocity.normalized * speed;

        float yAngle = Input.GetAxis("Mouse X") * 200 * sensitivity * Time.deltaTime;
        transform.rotation *= Quaternion.Euler(0, yAngle, 0);

        float deltaX = -Input.GetAxis("Mouse Y") * 200 * sensitivity * Time.deltaTime;

        if (Mathf.Abs(xAngle + deltaX) == 0)
            deltaX = 0;
        else if (Mathf.Abs(xAngle + deltaX) > 90)
            deltaX = (90 - Mathf.Abs(xAngle)) * Mathf.Sign(xAngle);

        xAngle += deltaX;

        cameraTransform.rotation *= Quaternion.Euler(deltaX, 0, 0);
        headTransform.rotation *= Quaternion.Euler(deltaX, 0, 0);
    }
}
