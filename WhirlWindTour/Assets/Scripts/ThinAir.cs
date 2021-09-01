using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThinAir : MonoBehaviour
{
    private float altitude;
    private Vector3 pos;
    private Vector3 vel;

    // Start is called before the first frame update
    void Start()
    {
        altitude = 5.5f;//so the bottom of the Gondola just touchesthe ground
        pos = new Vector3(0,altitude,0);
        vel = Vector3.zero;
        transform.position = pos;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1)) 
        { 
            altitude += .01f; 
            pos = new Vector3(transform.position.x, altitude, transform.position.z); 
        }

        if (Input.GetKey(KeyCode.Alpha0)) 
        { 
            altitude -= .01f; 
            pos = new Vector3(transform.position.x, altitude, transform.position.z); 
        }

        pos += vel * Time.deltaTime;
        transform.position = pos;
    }

    // Methods
    public void WindVelocity(float windSpeed, float windDirection) 
    { 
        vel = windSpeed * (new Vector3(Mathf.Sin(windDirection), 0, Mathf.Cos(windDirection))); 
    }
}
