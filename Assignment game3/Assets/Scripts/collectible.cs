using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class collectible : MonoBehaviour
{
    
    public static string cname = "ammopickup";
    public GameObject ammopickup;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 1 * Time.deltaTime, 0);
    }
}
