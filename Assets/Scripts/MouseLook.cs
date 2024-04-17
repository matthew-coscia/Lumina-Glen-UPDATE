using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    Transform playerbody;
    public GameObject Menu;
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
        float sens = Menu.GetComponent<MenuManager>().mouseSensitivityMulti;
        if (!PlayerHealth.isDead)
        {
            float moveX = Input.GetAxis("Mouse X") * 80 * Time.deltaTime * sens;
            float moveY = Input.GetAxis("Mouse Y") * 80 * Time.deltaTime * sens;

            playerbody.Rotate(Vector3.up * moveX);
            pitch -= moveY;
            pitch = Mathf.Clamp(pitch, -90f, 90f);
            transform.localRotation = Quaternion.Euler(pitch, 0, 0);
        }

        
    }
}
