using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHand : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            RotateToMouse();
        }
        else
        {
            transform.Rotate(0f, 0f, Time.deltaTime * 30); // 360/12 degrees/sec = 30 deg/sec
        }
    }

    // Methods
    void RotateToMouse()
    {
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(mouseWorldPos.y, mouseWorldPos.x);
        angle = angle * 180 / Mathf.PI;
        transform.rotation = Quaternion.Euler(0f, 0f, angle);
    }
}
