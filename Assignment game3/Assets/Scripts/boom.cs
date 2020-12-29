using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class boom : MonoBehaviour
{
    public AudioSource tankgun,ricochet;
    public static Time Currenttime;
    float cooldown = 3;
    public static float nextfiretime;
    public static int ammunition;
    public GameObject explosion, exphandler, impact;
    public Text displayammo;

    //raycast
    public float damage = 10f;
    public float range = 250f;
    public Camera cam;

    void Update()
    {
        if(Time.time > nextfiretime)
        {
            if (Input.GetButtonDown("Fire1") && move.playerhealth > 0 && move.gamelost != true)
            {
                nextfiretime = Time.time + cooldown;
                explode();
            }
        }
    }
    public void explode()
    {
        if(ammunition > 0 )
        {
            Shoot();
            tankgun.Play();
            ammunition-= 1;
            displayammo.text = "Ammunition: " + ammunition.ToString();
            GameObject a = Instantiate(exphandler) as GameObject;
            Instantiate(explosion, transform.position, transform.rotation);
            Destroy(a);
        }
        else
        {
            displayammo.text = "Out of ammo!";
        }
    }
    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                ricochet.Play();
                target.TakeDamage(damage);
                Instantiate(impact, target.transform.position, target.transform.rotation);
                
            }

        }   
    }
}
