using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FP_Controller : MonoBehaviour
{
    public Camera cam;
    public Rigidbody rb;

    public float move_speed = 0.01f;
    public float mouse_sensitivity = 1.0f;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //mouse x and y are not game-space x and y
        float ry = Input.GetAxis("Mouse X");
        float rx = Input.GetAxis("Mouse Y");
        
        float move_lr = Input.GetAxis("Horizontal");
        float move_fb = Input.GetAxis("Vertical");

        gameObject.transform.Rotate(0, ry, 0, Space.Self);
        cam.transform.Rotate(-rx, 0, 0, Space.Self);

        transform.Translate(new Vector3(move_lr * move_speed, 0, move_fb * move_speed));

    }
}
