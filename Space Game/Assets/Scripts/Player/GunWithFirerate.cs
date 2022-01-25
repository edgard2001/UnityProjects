using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunWithFirerate : MonoBehaviour
{
    //[SerializeField] private float maxFiringRange = 600, fireRateRPM = 600, shotTrailDuration = 1;
    //private float lastAttacked;
    //private Transform crosshairTransform;
    //private Camera cam;
    //private LineRenderer leftLaser, rightLaser;


    // Start is called before the first frame update
    void Start()
    {
        //lastAttacked = -(60f / fireRateRPM);
    }

    // Update is called once per frame
    void Update()
    {
        /*
        leftLaser.SetPosition(0, leftLaser.transform.position);
        rightLaser.SetPosition(0, rightLaser.transform.position);

        if (Input.GetMouseButton(0) && rotationAmount.magnitude < maxFireFieldRadius)
        {
            ShootMainGun();
        }
        else
        {
            impactPart.SetActive(false);
            leftLaser.SetPosition(1, leftLaser.transform.position);
            rightLaser.SetPosition(1, rightLaser.transform.position);
        }

        //regenerate shield
        if (Time.time - lastDamageTaken > shieldRegenDelay)
        {
            shield += Mathf.RoundToInt(shieldRegenRate * Time.deltaTime);
            shield = Mathf.Clamp(shield, 0, maxShield);
        }
        */
    }

    private void ShootMainGun()
    {
        /*
        if ((Time.time - lastAttacked) > shotTrailDuration)
        {
            impactPart.SetActive(false);
            leftLaser.SetPosition(1, leftLaser.transform.position);
            rightLaser.SetPosition(1, rightLaser.transform.position);
        }

        if ((Time.time - lastAttacked) < (60f / fireRateRPM)) return;

        RaycastHit hit;
        if (Physics.Raycast(cam.ScreenPointToRay(crosshairTransform.position), out hit, maxFiringRange))
        {
            if (hit.collider.tag != "Player")
            {
                if (hit.collider.tag == "enemy")
                {
                    //EnemyController target = hit.transform.GetComponent<EnemyController>();
                    EnemyController targetScript = hit.collider.GetComponentInParent<EnemyController>();
                    targetScript.TakeDamage(Mathf.RoundToInt(500 * Time.deltaTime));
                }
                impactPart.transform.position = hit.point;
                impactPart.transform.rotation = Quaternion.LookRotation(hit.normal);
                impactPart.SetActive(true);

                leftLaser.SetPosition(1, hit.point);
                rightLaser.SetPosition(1, hit.point);
            }
            else
            {
                impactPart.SetActive(false);
                leftLaser.SetPosition(1, cam.ScreenToWorldPoint(crosshairTransform.position) + cam.ScreenPointToRay(crosshairTransform.position).direction * maxFiringRange);
                rightLaser.SetPosition(1, cam.ScreenToWorldPoint(crosshairTransform.position) + cam.ScreenPointToRay(crosshairTransform.position).direction * maxFiringRange);
            }
        }
        else
        {
            impactPart.SetActive(false);
            leftLaser.SetPosition(1, cam.ScreenToWorldPoint(crosshairTransform.position) + cam.ScreenPointToRay(crosshairTransform.position).direction * maxFiringRange);
            rightLaser.SetPosition(1, cam.ScreenToWorldPoint(crosshairTransform.position) + cam.ScreenPointToRay(crosshairTransform.position).direction * maxFiringRange);
        }
        lastAttacked = Time.time;
        */
    }
}
