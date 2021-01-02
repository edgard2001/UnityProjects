using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private PlayerController playerScript;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 offset = new Vector3(0f, 10f, -4.5f);

    private void Start()
    {
        GameObject playerObject = GameObject.Find("Player Ship");
        playerScript = playerObject.GetComponent<PlayerController>();
        playerTransform = playerObject.GetComponent<Transform>();
    }


    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, playerTransform.position + offset + new Vector3(0f, 0f, -playerScript.VelocityZ * 0.03f), 20f * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, playerTransform.rotation, 10f * Time.deltaTime);

    }
}
