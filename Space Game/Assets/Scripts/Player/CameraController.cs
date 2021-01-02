using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerController playerScript;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 offset = new Vector3(0f, -1.4f, -3f);

    private void Start()
    {
        GameObject playerObject = GameObject.Find("Player");
        playerScript = playerObject.GetComponent<PlayerController>();
    }


    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position + offset + new Vector3(0f, 0f, -playerScript.Velocity * 0.03f);
    }
}
