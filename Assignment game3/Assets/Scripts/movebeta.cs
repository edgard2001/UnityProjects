using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class movebeta : MonoBehaviour
{
    //aim
    public float mouseSensitivity = 200f;
    public Transform aimturret;
    public Transform barrel;
    public Transform camera;
    float xrotation = 0f;

    //gameobjects
    public GameObject turret, bturret, body, deadsmoke;
    //movement
    public Rigidbody rbody;
    float forwardspeed, turnspeed;
    //values
    private int ammunition = boom.ammunition;
    public static int score, numofenemies, collectibles;
    public static int lives = 3;
    public static float playerhealth;
    public static bool stoprotate, gamelost;
    public static bool nextlevel, resetgame;
    public static bool invertaim = false;
    private bool collide;
    //time
    float currenttime = 0f;
    float startingtime;
    //text
    public Text displayammo, displayhealth;
    public GameObject panel, winpanel;
    public int starthealth = 250;
    public Slider slider;

    public move healthbar;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rbody = GetComponent<Rigidbody>();
        if (resetgame == true && nextlevel == false)
        {
            resetgame = false;
            lives = 5;
        }
        if (resetgame == false && nextlevel == true)
        {
            resetgame = true;
            lives = 3;
        }
        gamelost = false;
        stoprotate = false;
        boom.ammunition = 9;
        forwardspeed = 70f;
        turnspeed = 1f;
        playerhealth = starthealth;

        
        
        displayhealth.text = "Health: " + playerhealth.ToString();
        displayammo.text = "Ammunition: " + boom.ammunition.ToString();
      
     

    }
    // Update is called once per frame
    void Update()
    {

        //aim 
        if (move.stoprotate != true)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            if (invertaim == true)
            {
                xrotation -= mouseY;
            }
            else
            {
                xrotation += mouseY;
            }
            xrotation = Mathf.Clamp(xrotation, -20f, 30f);

            camera.localRotation = Quaternion.Euler(xrotation, 0, 0);
            aimturret.Rotate(0, mouseX, 0);

            barrel.Rotate(mouseY, 0, 0);
            bturret.transform.Rotate(0, 0, mouseY);
        }
        if (numofenemies == 0 && collectibles == 0)
        {
            win();
        }
    }

    void FixedUpdate()
    {
        float forwardmoveammount = 0;
        float turnAmmount = 0;

        forwardmoveammount = Input.GetAxis("Vertical") * forwardspeed;
        turnAmmount = Input.GetAxis("Horizontal") * turnspeed;

        transform.Rotate(0, turnAmmount, 0);
        rbody.AddRelativeForce(0, 0, forwardmoveammount);
    }
    

    public void playertakedamage(float ammountofdmg)
    {
        playerhealth -= ammountofdmg;
        displayhealth.text = "Health: " + playerhealth.ToString();

        if (playerhealth <= 0)
        {
            playerdeath();
        }
    }

    public void playerdeath()
    {
        lives--;
        Debug.Log(lives);
        forwardspeed = 0;
        turnspeed = 0;
        Instantiate(deadsmoke, transform.position, Quaternion.Euler(new Vector3(-45, 0, 0)));
        if (lives > 0 && nextlevel == false)
        {
            playerhealth = 150f;
            SceneManager.LoadScene(1);
        }
        if (lives < 0)
        {
            freezeplayer();
            panel.SetActive(true);
    
        }
        if (lives > 0 && nextlevel == true)
        {
            playerhealth = 150f;
            SceneManager.LoadScene(2);
        }
    }
    private void win()
    {
        resetgame = false;
        winpanel.SetActive(true);
        freezeplayer();
 
    }
    private void freezeplayer()
    {
        gamelost = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        forwardspeed = 0f;
        turnspeed = 0f;
        stoprotate = true;
    }
    public void SetMaxHealth(int health)
    {
        slider.maxValue = health;
        slider.value = health;
    }
    public void SetHealth(int health)
    {
        slider.value = playerhealth;
    }
}

