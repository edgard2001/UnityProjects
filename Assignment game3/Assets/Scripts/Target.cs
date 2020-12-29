using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class Target : MonoBehaviour
{
    public float health = 30f;
    public int scorevalue = 20;

    //ai
    public Rigidbody ai;
    public Transform destination;
    NavMeshAgent navmeshagent;

    //gameobjects
    public GameObject gameobjturret, body,deadsmoke;
    //text
    public Text displaycore,displayenemies;

    private void Start()
    {
        ai = GetComponent<Rigidbody>();
        navmeshagent = this.GetComponent<NavMeshAgent>();
        displayenemies.text = "Enemies Left: " + move.numofenemies.ToString();

    }
    private void Update()
    {
        if (navmeshagent == null)
        {
            Debug.Log("navmesh not attached to game object ya spastic ");
        }
        else
        {
             SetDestination();
        }

        if(health <= 0)
        {
            navmeshagent.speed = 0;
        }
    }

    public void TakeDamage(float ammount)
    {
        health -= ammount;
        if (health <= 0f)
        {
            Die();
        }
        void Die()
        {
            move.numofenemies--;
            move.score += scorevalue;    
            displaycore.text = "Score: " + move.score;
            displayenemies.text = "Enemies Left: " + move.numofenemies.ToString();
            Destroy(gameobjturret);
            Instantiate(deadsmoke, transform.position, Quaternion.Euler(new Vector3(-45, 0, 0)));
            Destroy(body, 10);
        }
    }
    private void SetDestination()
    {
        if (destination != null)
        {
            Vector3 targetVector = destination.position;
            navmeshagent.SetDestination(targetVector);
        }
    }
}
