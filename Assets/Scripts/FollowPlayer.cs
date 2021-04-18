using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public GameObject playerObj;
    public float smootTime = 0.3f;
    Vector2 velocity = Vector2.zero;

    public int yOffSet;
    public int xoff;


    private void Update()
    {
        Vector2 targetPosition = playerObj.transform.TransformPoint(new Vector3(0,yOffSet));
        if (targetPosition.y < transform.position.y)
        {
            return;
        }
        targetPosition = new Vector3(0,targetPosition.y);
        transform.position = Vector2.SmoothDamp(transform.position,targetPosition,ref velocity,smootTime);

        transform.position = new Vector3(transform.position.x,transform.position.y,-10);
    }


}
