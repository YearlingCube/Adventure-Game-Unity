using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpThruPlatform : MonoBehaviour
{
    public Transform Player;

    // Update is called once per frame
    void Update()
    {
        if (Player.position.y > transform.position.y)
        {
            transform.gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        else
        {
            transform.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }

    }
}
