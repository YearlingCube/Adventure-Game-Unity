using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hearts : MonoBehaviour {

    public int health;
    public int numOfHearts;

    public Image[] heartss;
    public Sprite Full_Heart;
    public Sprite Empty_Heart;

    void Update()
    {
        for (int i = 0; i < heartss.Length; i++)
        {
            if(i < numOfHearts)
            {
                heartss[i].enabled = true;

            }
            else
            {
                heartss[i].enabled = false;
            }

            if (i < numOfHearts)
            {
                heartss[i].enabled = true;
            }
            else
            {
                heartss[i].enabled = false;
            }
        }
        }

    }
