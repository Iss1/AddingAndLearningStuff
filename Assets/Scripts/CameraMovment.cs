﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovment : MonoBehaviour
{
    /*
    // VARIABLES
    public float panSpeed = 4.0f;

    private Vector3 mouseOrigin;
    private bool isPanning;


    // Update is called once per frame
    void Update()
    {

        
        if (Input.GetMouseButtonDown(1))
        {
            //right click was pressed    
            mouseOrigin = Input.mousePosition;
            isPanning = true;
        }


        // cancel on button release
        if (!Input.GetMouseButton(1))
        {
            isPanning = false;
        }

        //move camera on X & Y
        if (isPanning)
        {
            Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseOrigin);

            // update x and y but not z
            Vector3 move = new Vector3(pos.x * panSpeed, pos.y * panSpeed, 0);

            Camera.main.transform.position += pos;
        }
    }
}
*/

    public float speed = 10.0f;

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            if (Input.GetAxis("Mouse X") > 0)
            {
                transform.position -= new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed,
                                           Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed);
            }

            else if (Input.GetAxis("Mouse X") < 0)
            {
                transform.position -= new Vector3(Input.GetAxisRaw("Mouse X") * Time.deltaTime * speed,
                                           Input.GetAxisRaw("Mouse Y") * Time.deltaTime * speed);
            }
        }
    }
}