using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour {
    // This Script Controls Player's Health UI

    public int numOfHearts = 6;
    // This All of the Heart Images
    public Image Full_Heart = null;
    public Image Full_Heart1 = null;
    public Image Full_Heart2 = null;
    public Image Half_Heart = null;
    public Image Half_Heart1 = null;
    public Image Half_Heart2 = null;
    public Image Empty_Heart = null;
    public Image Empty_Heart1 = null;
    public Image Empty_Heart2 = null;
    // Gets The Player

    public GameObject Player;
    void Update()
    {
        // Sets Number Of Hearts To Player Health
        numOfHearts = Player.GetComponent<Bandit>().PlayerHealth;
        // Switches Based Off Player Health
        switch (numOfHearts)
        {
            case 6:
                break;
            case 5:
                Full_Heart2.enabled = false;
                break;
            case 4:
                Half_Heart2.enabled = false;
                break;
            case 3:
                Full_Heart1.enabled = false;
                break;
            case 2:
                Half_Heart1.enabled = false;
                break;
            case 1:
                Full_Heart.enabled = false;
                break;
            case 0:
                Half_Heart.enabled = false;
                break;
        }
    }

}