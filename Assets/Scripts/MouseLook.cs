using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    Transform playerbody;
    public float MouseSensitivity = 100;

    float pitch = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        playerbody = transform.parent.transform;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = Input.GetAxis("Mouse X") * MouseSensitivity * Time.deltaTime;
        float moveY = Input.GetAxis("Mouse Y") * MouseSensitivity * Time.deltaTime;


        // yaw
        playerbody.Rotate(Vector3.up * moveX);
        
        // pitch
        pitch -= moveY;

        pitch = Mathf.Clamp(pitch, -90f, 90f);
        transform.localRotation = Quaternion.Euler(pitch, 0, 0);
        
    }
}