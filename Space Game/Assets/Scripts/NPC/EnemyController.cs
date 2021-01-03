using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private float attackRange = 100;
    [SerializeField] private Vector3 velocity;
    [SerializeField] private float roll, pitch, yaw;
 
    private bool chasingTarget, firing;
   

    void FixedUpdate()
    {
        Vector3 targetDisplacement = targetTransform.position - transform.position;
        targetDisplacement = transform.InverseTransformDirection(targetDisplacement);
        print(targetDisplacement);

        float targetDistance = targetDisplacement.magnitude;

        if (targetDistance > attackRange)
        {
            chasingTarget = true;
            firing = false;
        }
        else {
            chasingTarget = false;
            firing = true;
        }

        if (chasingTarget)
        {
            velocity.z = Mathf.Lerp(velocity.z, 100, 5 * Time.deltaTime);
        }
        else if (firing)
        {
            velocity.z = Mathf.Lerp(velocity.z, 0, 5 * Time.deltaTime);
        }

        if (targetDisplacement.normalized.y - Vector3.forward.y < -0.001)
        {
            pitch = -300 * (Vector3.forward.y - targetDisplacement.normalized.y) / 1 * Time.deltaTime;
        }
        else if (targetDisplacement.normalized.y - Vector3.forward.y > 0.001)
        {
            pitch = 300 * (targetDisplacement.normalized.y - Vector3.forward.y) / 1 * Time.deltaTime;
        }

        if (targetDisplacement.normalized.x - Vector3.forward.x < -0.001)
        {
            yaw = -300 * (Vector3.forward.x - targetDisplacement.normalized.x) / 1 *Time.deltaTime;
        }
        else if (targetDisplacement.normalized.x - Vector3.forward.x > 0.001)
        {
            yaw = 300 * (targetDisplacement.normalized.x - Vector3.forward.x) / 1 * Time.deltaTime;
        }

        Vector3 rotations = new Vector3(-pitch, yaw, roll);
        transform.Rotate(rotations, Space.Self);

        transform.Translate(velocity.z * Vector3.forward * Time.deltaTime);

    }


}
