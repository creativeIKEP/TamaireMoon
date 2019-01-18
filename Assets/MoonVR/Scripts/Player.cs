using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour 
{
    private int score = 0;
    public float movespeed;

    private void Update()
    {
        if(Input.GetKey(KeyCode.W)){
            transform.position = transform.position + Camera.main.transform.forward * movespeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position = transform.position - Camera.main.transform.forward * movespeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position = transform.position + Camera.main.transform.right * movespeed * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position = transform.position - Camera.main.transform.right * movespeed * Time.deltaTime;
        }
    }

    public void PlayerScoreUp()
    {
        score += 10;
    }

    public int GetPlayerScore()
    {
        return score;
    }
}
