using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotion : MonoBehaviour
{
    private ITargetFollower _follower;

    private void Awake()
    {
        Transform camera = gameObject.GetComponent<Transform>();
        Transform target = GameObject.Find("Player Character").GetComponent<Transform>();
        Vector3 offset = new Vector3(0f, 1f, -10f);

        _follower = new EaseInTargetFollower(camera, target, offset, 20f);
    }

    // Update is called once per frame
    void Update()
    {
        _follower.MoveToNextPosition();
    }
    
}
