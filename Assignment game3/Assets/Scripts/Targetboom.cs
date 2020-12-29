using UnityEngine;
using UnityEngine.UI;

public class Targetboom : MonoBehaviour
{

    public AudioSource aigun;
    public GameObject player;
    //values
    public float nextfiretimeai;
    float aicooldown = 3;
    //private float aiturrot = 10;
    public Transform transturret;
    //gameobjects
    public GameObject explosion, exphandlerai;
    string hittarget;

    public float range = 110f;
    public int aidamage = 10;

    private void Start()
    {
        if(move.nextlevel == true)
        {
            aidamage = 15;
        }
    }

    private void Update()
    {
        string targetname = hittarget;
        float dist = Vector3.Distance(transturret.position, player.transform.position);

            if (dist < 150)
            {
                transturret.LookAt(player.transform.position);
                bool lockon = true;
            if (lockon == true)
                {
                    if (Time.time > nextfiretimeai)
                    {
                        nextfiretimeai = Time.time + aicooldown;
                        explodeai();
                    }
                }
            } 
    }

    public void explodeai()
    {
        aiShoot();
        aigun.Play();
        GameObject b = Instantiate(exphandlerai) as GameObject;
        Instantiate(explosion, transform.position, transform.rotation);
        Destroy(b); 

    }

    void aiShoot()
    {
        RaycastHit aihit;
        if (Physics.Raycast(this.transform.position, this.transform.forward, out aihit, range))
        {
            string targetname = aihit.transform.name;
            hittarget = targetname;
            Debug.Log(targetname);
            move player = aihit.transform.GetComponent<move>();
            if (player != null)
            {
                player.playertakedamage(aidamage);
                Instantiate(explosion, player.transform.position, player.transform.rotation);
            }
        }
    }

  
    
}
