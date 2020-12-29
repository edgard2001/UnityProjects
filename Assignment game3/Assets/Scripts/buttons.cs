using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttons : MonoBehaviour
{
    public GameObject losepanel;
    public Text invertaim;
    public void retry()
    {
        SceneManager.LoadScene(1);
    }
    public void playgame()
    {
        move.resetgame = true;
        SceneManager.LoadScene(1);
        move.nextlevel = false;
    }
    public void nextlevel()
    {
        SceneManager.LoadScene(2);
        move.nextlevel = true;
    }
    public void menu()
    {
        SceneManager.LoadScene(0);
        losepanel.SetActive(false);
    }
    public void invert()
    {
        if (move.invertaim == false)
        {
            move.invertaim = true;
            invertaim.text = "Inverted aim: off";
        }
        else
        {
            move.invertaim = false;
            invertaim.text = "Inverted aim: on";
        }
    }
}
