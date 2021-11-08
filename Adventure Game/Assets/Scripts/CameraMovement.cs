using UnityEngine;

public class CameraMovement : MonoBehaviour
{
     public Transform trackingTarget;

     public float xOffset;

     public float yOffset;

     public float xfollowSpeed;
     public float yfollowSpeed;

    void FixedUpdate()
    {
        float xTarget = trackingTarget.position.x + xOffset;
        float yTarget = trackingTarget.position.y + yOffset;

        float xNew = Mathf.Lerp(transform.position.x, xTarget, Time.deltaTime * xfollowSpeed);
        float yNew = Mathf.Lerp(transform.position.y, yTarget, Time.deltaTime * yfollowSpeed);

        transform.position = new Vector3(xNew, yNew, transform.position.z);
    }
}
