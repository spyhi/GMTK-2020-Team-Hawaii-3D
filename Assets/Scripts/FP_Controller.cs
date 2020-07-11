using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_Controller : MonoBehaviour
{
    public Camera cam;
    public Rigidbody rb;

    public float move_speed = 0.01f;
    public float mouse_sensitivity = 1.0f;
    public float jump_power = 1.0f;
    public float max_ry = 80;
    public float min_ry = -80;

    private bool grounded = true;


    void Update()
    {
        //mouse x and y are not game-space x and y
        float ry = Input.GetAxis("Mouse X") * mouse_sensitivity;
        float rx = Input.GetAxis("Mouse Y") * mouse_sensitivity;  
        float move_lr = Input.GetAxis("Horizontal");
        float move_fb = Input.GetAxis("Vertical");
        bool jump = Input.GetButtonDown("Jump");
        float cam_angle = cam.transform.rotation.x;

        //check ground
        if (Physics.Raycast(transform.position, -transform.up, 1.5f)){
            grounded = true;
        }
        else{
            grounded = false;
        }
        
        //rotations
        gameObject.transform.Rotate(0, ry, 0, Space.Self);
        rx = Mathf.Clamp(rx, min_ry, max_ry);
        cam.transform.eulerAngles = cam.transform.eulerAngles + new Vector3(-rx, 0, 0f);

        //movement
        transform.Translate(new Vector3(move_lr * move_speed, 0, move_fb * move_speed));

        //jump
        if(jump && grounded){
            rb.velocity = new Vector3(0, jump_power, 0);
        }
    }
}
