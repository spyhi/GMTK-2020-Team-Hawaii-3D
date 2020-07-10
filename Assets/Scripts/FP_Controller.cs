﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_Controller : MonoBehaviour
{
    public Camera cam;
    public Rigidbody rb;

    public float move_speed = 0.01f;
    public float mouse_sensitivity = 1.0f;
    public float jump_power = 1.0f;

    private bool grounded = true;


    void Update()
    {
        //mouse x and y are not game-space x and y
        float ry = Input.GetAxis("Mouse X");
        float rx = Input.GetAxis("Mouse Y");  
        float move_lr = Input.GetAxis("Horizontal");
        float move_fb = Input.GetAxis("Vertical");
        bool jump = Input.GetButtonDown("Jump");

        //check ground
        if (Physics.Raycast(transform.position, -transform.up, 1.5f)){
            grounded = true;
        }
        else{
            grounded = false;
        }
        
        //rotations
        gameObject.transform.Rotate(0, ry * mouse_sensitivity, 0, Space.Self);
        cam.transform.Rotate(-rx * mouse_sensitivity * 0.75f, 0, 0, Space.Self);

        //movement
        transform.Translate(new Vector3(move_lr * move_speed, 0, move_fb * move_speed));

        //jump
        if(jump && grounded){
            rb.velocity = new Vector3(0, jump_power, 0);
        }
    }
}
