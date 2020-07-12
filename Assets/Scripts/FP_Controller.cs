using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_Controller : MonoBehaviour
{
    public Camera cam;
    public Rigidbody rb;
    public bool movementAllowed = true;

    public float move_speed = 0.02f;
    public float mouse_sensitivity = 1.0f;
    public float jump_power = 1.5f;

    private bool grounded = true;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab)) //button to toggle cursor lock, for testing
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            } else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if (movementAllowed)
        {
            //mouse x and y are not game-space x and y
            float ry = Input.GetAxis("Mouse X") * mouse_sensitivity;
            float rx = Input.GetAxis("Mouse Y") * mouse_sensitivity;
            float move_lr = Input.GetAxis("Horizontal") * Time.deltaTime * 120;
            float move_fb = Input.GetAxis("Vertical") * Time.deltaTime * 120;
            bool jump = Input.GetButtonDown("Jump");
            float cam_angle = cam.transform.rotation.x;

            //check ground
            if (Physics.Raycast(transform.position, -transform.up, 2.55f))
            {
                grounded = true;
            }
            else
            {
                grounded = false;
            }

            //rotations
            gameObject.transform.Rotate(0, ry, 0, Space.Self);
            //Nightmare rotation clamping, do not touch
            if (rx < 0 && cam.transform.eulerAngles.x - rx > 80 && cam.transform.eulerAngles.x - rx < 180)
            {
                cam.transform.eulerAngles = new Vector3(80, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);
            }
            else if (rx > 0 && 180 < cam.transform.eulerAngles.x - rx && cam.transform.eulerAngles.x - rx < 280)
            {
                cam.transform.eulerAngles = new Vector3(280, cam.transform.eulerAngles.y, cam.transform.eulerAngles.z);
            }
            else
            {
                cam.transform.eulerAngles = cam.transform.eulerAngles + new Vector3(-rx, 0, 0f);
            }

            //movement
            transform.Translate(new Vector3(move_lr * move_speed, 0, move_fb * move_speed));

            //jump
            if (jump && grounded)
            {
                rb.velocity = new Vector3(0, jump_power, 0);
            }
        }
    }
}
