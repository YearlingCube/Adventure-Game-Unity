using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pos1;
    public Transform pos2;
    public Transform startpos;
    public float speed;

    Vector3 nextPos;

    float waitTime = 1;
    bool isWaiting = false;

    // Start is called before the first frame update
    void Start()
    {
        nextPos = startpos.position;
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.position == pos1.position)
        {
            if (isWaiting == false)
            {
            waitTime = Time.time + 1;
            isWaiting = true;
            }
            if (waitTime < Time.time)
            {
            nextPos = pos2.position;
                isWaiting = false;
            }
            
        }
        if (transform.position == pos2.position)
        {
            if (isWaiting == false)
            {
                waitTime = Time.time + 1;
                isWaiting = true;
            }
            if (waitTime < Time.time)
            {
            nextPos = pos1.position;
                isWaiting = false;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, nextPos, speed * Time.deltaTime);
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(pos1.position, pos2.position);
    }
}