using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    Rigidbody2D rb;

    public float jumpHeight;
    public float gravity = 1;
    float jumpVelocity;

    bool isDragging = false;

    Vector2 touchPosition;
    Vector2 playerPosition;
    Vector2 dragPosition;

    StairsManager stairsManager;

    public GameObject jumpEffectPrefab;
    public GameObject DeadEffectPrefab;
    public GameObject stairEffectPrefab;

    bool isDead = false;
    bool isStart = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        stairsManager = GameObject.Find("Stairs").GetComponent<StairsManager>();
    }

    private void Update()
    {
        WaitToTouch();
        if (isDead)
        {
            return;
        }
        if (!isStart)
        {
            return;
        }
        GetInput();
        MovePlayer();
        AddGravityToPlayer();
        DeadCheck();
    }

    private void WaitToTouch()
    {
        if (!isStart)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isStart = true;
                GameObject.Find("GameManager").GetComponent<GameManager>().GameStart();
            }
        }
    }

    private void DeadCheck()
    {
        if (isDead == false && transform.position.y < Camera.main.transform.position.y - 10)
        {
            isDead = true;

            rb.isKinematic = true;
            rb.velocity = Vector2.zero;

            Destroy(Instantiate(DeadEffectPrefab, transform.position, Quaternion.identity), 1.0f);
            GameObject.Find("GameManager").GetComponent<GameManager>().GameOver();
        }
    }

    private void GetInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isDragging = true;
            touchPosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            playerPosition = transform.position;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }
    }

    private void MovePlayer()
    {
        if (isDragging == true)
        {
            dragPosition = Camera.main.ScreenToWorldPoint(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            transform.position = new Vector2(playerPosition.x + (dragPosition.x - touchPosition.x), transform.position.y);
            if (transform.position.x < -4)
            {
                transform.position = new Vector2(-4, transform.position.y);
            }
            if (transform.position.x > 4)
            {
                transform.position = new Vector2(4, transform.position.y);
            }
        }
    }

    void AddGravityToPlayer()
    {
        rb.velocity = new Vector2(0, rb.velocity.y - (gravity * gravity));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "stair")
        {
            if (rb.velocity.y <= 0)
            {
                Jump();
                Addscore();
                Effect(other);
                ChangeBackgroundColor(other);
                DestroyAndMakeStair(other);
            }

        }
    }

    private void Effect(Collider2D stair)
    {
        GameObject stairEffectobj = Instantiate(stairEffectPrefab, transform.position, Quaternion.identity);
        stairEffectobj.transform.localScale = new Vector2(stair.transform.localScale.x,stair.transform.localScale.y);
        Destroy(stairEffectobj,0.5f); 

        Destroy(Instantiate(jumpEffectPrefab, transform.position, Quaternion.identity), 0.5f);
    }

    void Addscore()
    {
        GameObject.Find("GameManager").GetComponent<ScoreManager>().AddScore(1);
    }

    private void ChangeBackgroundColor(Collider2D other)
    {
        Color color = other.gameObject.GetComponent<SpriteRenderer>().color;
        color.r += 0.1f;
        color.g += 0.1f;
        color.b += 0.1f;

        Camera.main.backgroundColor = color; //ve ya other.gameObject.GetComponent<SpriteRenderer>().color;
    }

    void Jump()
    {
        jumpVelocity = gravity * jumpHeight;
        rb.velocity = new Vector2(0, jumpVelocity);
        gravity += 0.005f;
    }

    void DestroyAndMakeStair(Collider2D stair)
    {
        Destroy(stair.gameObject);
        stairsManager.MakeNewStair();
    }

}
