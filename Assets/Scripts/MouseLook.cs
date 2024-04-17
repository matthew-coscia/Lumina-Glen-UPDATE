using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    Transform playerbody;
    public GameObject MenuManager;
    private float mouseSensitivity;

    float pitch = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        mouseSensitivity = MenuManager.GetComponent<MenuManager>().mouseSensitivityMulti;
        playerbody = transform.parent.transform;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        
    }

    // Update is called once per frame
    void Update()
    {
        mouseSensitivity = MenuManager.GetComponent<MenuManager>().mouseSensitivityMulti;
        float moveX = Input.GetAxis("Mouse X") * 80 * Time.deltaTime * mouseSensitivity;
        float moveY = Input.GetAxis("Mouse Y") * 80 * Time.deltaTime * mouseSensitivity;


        // yaw
        playerbody.Rotate(Vector3.up * moveX);
        
        // pitch
        pitch -= moveY;

        pitch = Mathf.Clamp(pitch, -90f, 90f);
        transform.localRotation = Quaternion.Euler(pitch, 0, 0);
        
    }
}
