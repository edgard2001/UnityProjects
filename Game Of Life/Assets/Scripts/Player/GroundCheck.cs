using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool IsGrounded;// { get; set; }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsGrounded = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsGrounded = false;
    }
}
