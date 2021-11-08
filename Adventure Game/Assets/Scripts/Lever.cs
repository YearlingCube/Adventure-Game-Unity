using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public Sprite FlippedState;
    public Sprite NotFlippedState;

    public Transform Door;

    public bool Flipped = false;
    Vector2 pos1;
    public Vector2 awayPos;
    private void Start()
    {
        pos1 = Door.transform.position;
    }
    public void FlipLever()
    {
        if (Flipped == false)
        {
            Flipped = true;
        gameObject.GetComponent<SpriteRenderer>().sprite = NotFlippedState;
            Door.transform.position = awayPos;

        }
        else if (Flipped == true)
        {
            Flipped = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = FlippedState;
            Door.transform.position = pos1;
        }
    }
}
