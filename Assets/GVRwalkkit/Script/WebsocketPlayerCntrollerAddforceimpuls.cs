﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WebsocketPlayerCntrollerAddforceimpuls : MonoBehaviour
{
    private Rigidbody rigidbody;
    private Animator animatorh;
    // Use this for initialization
    [SerializeField]
    private float m_jumpForce = 8.0f;


    void Start()
    {

        rigidbody = GetComponent<Rigidbody>();
        animatorh = GetComponent<Animator>();

    }
    float inputHorizontal;
    float inputVertical;

    // WASDで移動する
    float x = 0.0f;
    float z = 0.0f;
    //onece addforce
    bool OneX;
    bool OneZ;
    bool OneY;

    public float movespeed = 10f;

    // Update is called once per frame
    void Update()
    {

        Vector3 cameraForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 cameraRight = Vector3.Scale(Camera.main.transform.right, new Vector3(1, 0, 1)).normalized;
        inputHorizontal = Input.GetAxisRaw("Horizontal");
        inputVertical = Input.GetAxisRaw("Vertical");

        //rigidbody.AddForce(cameraForward * movespeed * SmartPhoneInput.PadGradX, ForceMode.Impulse);
        Debug.Log("OneXZ: " + OneX + OneZ);

        //Idle point frag
        if (0.4 > SmartPhoneInput.PadGradX && SmartPhoneInput.PadGradX > -0.4) { OneX = true; }

        if (0.5 > SmartPhoneInput.PadGradZ && SmartPhoneInput.PadGradZ > -0.5) { OneZ = true; }

        if (SmartPhoneInput.PadGradY > -0.5) { OneY = true; }


        //move forward
        if (SmartPhoneInput.PadGradX > 0.4 && OneX == true)
        {
            //transform.position += cameraForward * movespeed;
            rigidbody.AddForce(cameraForward * movespeed, ForceMode.Impulse);
            OneX = false;
            //rigidbody.AddForce(cameraForward * 100, ForceMode.Force);
            //z += 1.0f;
            // animatorh.SetBool("iswalk", true); 
            Debug.Log("up: " + SmartPhoneInput.PadGradX);
        }
        else
        {
            // animatorh.SetBool("iswalk", false);
        }
        //move back
        if (SmartPhoneInput.PadGradX < -0.4 && OneX == true)
        {
            //transform.position -= cameraForward * movespeed;
            rigidbody.AddForce(cameraForward * -movespeed, ForceMode.Impulse);
            OneX = false;
            //z -= 1.0f;
            //animatorh.SetBool("isback", true);
        }
        else
        {
            //animatorh.SetBool("isback", false);
            Debug.Log("down: " + SmartPhoneInput.PadGradX);
        }

        //move left
        if (SmartPhoneInput.PadGradZ > 0.5 && OneZ == true)
        {
            //x -= 1.0f;
            //transform.position -= cameraRight * movespeed;
            rigidbody.AddForce(cameraRight * -movespeed, ForceMode.Impulse);
            OneZ = false;
            //animatorh.SetBool("isright", true);
        }
        else
        {
            // animatorh.SetBool("isright", false);
            Debug.Log("right: " + SmartPhoneInput.PadGradZ);

        }

        //move right
        if (SmartPhoneInput.PadGradZ < -0.5 && OneZ == true)
        {
            //transform.position += cameraRight * movespeed;
            rigidbody.AddForce(cameraRight * movespeed, ForceMode.Impulse);
            OneZ = false;
            //x += 1.0f;
            //transform.Rotate(0, -10, 0);

            //animatorh.SetBool("isleft", true);
        }
        else
        {
            // animatorh.SetBool("isleft", false);

            Debug.Log("left: " + SmartPhoneInput.PadGradZ);


        }


        //jump
        if (SmartPhoneInput.PadGradY < -0.5 && OneY == true)
        {

            //rigidbody.AddForce(cameraRight * movespeed, ForceMode.Impulse);
            OneY = false;
            //rigidbody.velocity = Vector3.zero;

            Vector3 jumpVec = Vector3.up * m_jumpForce;
            rigidbody.AddForce(jumpVec, ForceMode.VelocityChange);

        }
        else
        {
            // animatorh.SetBool("isleft", false);

            Debug.Log("jump: " + SmartPhoneInput.PadGradY);


        }

    }
}