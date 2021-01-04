using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorScript : MonoBehaviour
{

    private Vector3 mousePosition;
    public float moveSpeed = 0.1f;

    // Update is called once per frame
    void Update()
    {
        mousePosition = Input.mousePosition;
        transform.position = Vector2.Lerp(transform.position, mousePosition, moveSpeed);

    }
}
