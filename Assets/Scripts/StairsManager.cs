using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class StairsManager : MonoBehaviour
{

    public GameObject stairPrefab;
    int stairIndex = 0;

    float stairWidth = 3f;
    float stairHeight = 0.5f;

    float hue;

    private void Start()
    {
        InitColor();
        for (int i = 0; i < 2; i++)
        {
            MakeNewStair();
        }
    }

    private void InitColor()
    {
        hue = Random.Range(0, 1f);
        Camera.main.backgroundColor = Color.HSVToRGB(hue, 0.6f, 0.8f);
    }

    public void MakeNewStair()
    {
        int randomPositionX;
        if (stairIndex == 0)
        {
            randomPositionX = 0;
        }
        else
        {
            randomPositionX = Random.Range(-4, 4);
        }

        Vector2 newPosition = new Vector2(randomPositionX, stairIndex * 5);

        GameObject newStair = Instantiate(stairPrefab, newPosition, Quaternion.identity);
        newStair.transform.SetParent(transform);
        newStair.transform.localScale = new Vector2(stairWidth, stairHeight);

        SetColor(newStair);

        DecreaseStairWidth();

        stairIndex++;
    }


    private void DecreaseStairWidth()
    {
        stairWidth -= 0.01f;

        if (stairWidth < 1)
        {
            stairWidth = 1;
        }
    }

    private void SetColor(GameObject newStair)
    {

        if (Random.Range(0, 3) != 0 || stairIndex == 0)
        {
            hue += 0.11f;
            if (hue >= 1)
            {
                hue -= 1;
            }
        }

        newStair.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(hue, 0.6f, 0.9f);
        if (Camera.main.backgroundColor == newStair.GetComponent<SpriteRenderer>().color)
        {
            print("Oldu");
            newStair.GetComponent<SpriteRenderer>().color = Color.HSVToRGB(hue + 0.08f, 0.6f, 0.8f);
        }
    }
}
