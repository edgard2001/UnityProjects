using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animtest : MonoBehaviour
{
    private Animator animator;
    private bool isrotated = false;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //thrusters animations
        if (Input.GetKeyDown("p") )
        {
            if (isrotated == false)
            {
                isrotated = true;
                animator.SetBool("isRotating", true);
                animator.SetBool("isBackRotate", false);
            }
            else if (isrotated == true)
            {
                isrotated = false;
                animator.SetBool("isRotating", false);
                animator.SetBool("isBackRotate", true);
            }
        }
    }
    
}
