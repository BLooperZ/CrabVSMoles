using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] balls;

    void Start()
    {
        balls = GameObject.FindGameObjectsWithTag("Ball");
    }
    // Start is called before the first frame update
    public void Strike()
    {
        // Destroy the first ball in the array and shift the rest of the balls to the left
        Destroy(balls[0]);
        for (int i = 1; i < balls.Length; i++)
        {
            balls[i - 1] = balls[i];
        }
    }
}
