using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Stair : MonoBehaviour
{

    Vector2 startPosition;
    Vector2 targetPosition;
    float randomFloat;
    float smootTime = 0.1f;
    Vector2 velocity = Vector2.zero;

    private void Start()
    {

        targetPosition = transform.position;
        if (Random.Range(0,2) == 0)
        {
            randomFloat = -10;
        }
        else
        {
            randomFloat = 10;
        }

        startPosition = new Vector2(targetPosition.x + randomFloat,targetPosition.y);
        transform.position = startPosition;

    }//start

    private void Update()
    {

        if (Vector2.Distance(targetPosition,transform.position) > 0.01f)
        {
            MoveToTargetPosition();
        }

    }//update

    private void MoveToTargetPosition()
    {
        transform.position = Vector2.SmoothDamp(transform.position, targetPosition, ref velocity, smootTime);
    }
}
