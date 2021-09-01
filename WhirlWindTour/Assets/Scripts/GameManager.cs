﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public GameObject windVane; //note that this references the prefab WindVane, which is not a GameObject in the Hierarchy, it is in Project > Assets > Models folder 

    public GameObject[,] windVaneList;  //[,0] holds wind vanes on outer ring, [,1] holds wind vanes on inner ring

    public GameObject thinAirBalloon;

    private const int numWindVanes = 24; //actually this is the number of wind vanes on a ring, but there are two rings, so 48 wind vanes total
    private float windSpeed;  
    private float windDirection; //this is the angle that the wind velocity vector makes with the z axis, which is 0 initially
    private GameObject balloon;

    // Start is called before the first frame update
    void Start()
    {
        windSpeed = 0;
        windDirection = 0;

        windVaneList = new GameObject[numWindVanes,2];

        for (int i = 0; i < numWindVanes; i++)
        {
            windVaneList[i,0] = Instantiate(windVane, new Vector3(47 * Mathf.Sin(360f / numWindVanes * i * Mathf.Deg2Rad), 5, 47 * Mathf.Cos(360f / numWindVanes * i * Mathf.Deg2Rad)), Quaternion.identity);
            windVaneList[i,0].transform.localScale = new Vector3(5, 5, 5);
            windVaneList[i,1] = Instantiate(windVane, new Vector3(37 * Mathf.Sin(360f / numWindVanes * i * Mathf.Deg2Rad), 5, 33 * Mathf.Cos(360f / numWindVanes * i * Mathf.Deg2Rad)), Quaternion.identity);
            windVaneList[i,1].transform.localScale = new Vector3(5, 5, 5);
        }

        balloon = Instantiate(thinAirBalloon, Vector3.zero, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            windSpeed += .1f;  //currently does nothing
            Debug.Log("windSpeed has increased to: " + windSpeed);
            balloon.GetComponent<ThinAir>().WindVelocity(windSpeed, windDirection);
        }
      
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            windSpeed -= .1f;  //currently does nothing
            Debug.Log("windSpeed has decreased to: " + windSpeed);
            balloon.GetComponent<ThinAir>().WindVelocity(windSpeed, windDirection);
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            windDirection -= .1f; //note that this represents an angle in radians, not degrees

            for (int i = 0; i < numWindVanes; i++)
            {
                windVaneList[i,0].transform.rotation = Quaternion.Euler(new Vector3(0, windDirection * Mathf.Rad2Deg, 0));
                windVaneList[i,1].transform.rotation = Quaternion.Euler(new Vector3(0, windDirection * Mathf.Rad2Deg, 0));
            }

            balloon.GetComponent<ThinAir>().WindVelocity(windSpeed, windDirection);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        { 
             windDirection += .1f; //note that this represents an angle in radians, not degrees

            for (int i = 0; i < numWindVanes; i++)
            { 
                windVaneList[i,0].transform.rotation = Quaternion.Euler(new Vector3(0, windDirection * Mathf.Rad2Deg, 0));
                windVaneList[i,1].transform.rotation = Quaternion.Euler(new Vector3(0, windDirection * Mathf.Rad2Deg, 0));
            }

            balloon.GetComponent<ThinAir>().WindVelocity(windSpeed, windDirection);
        }

    }
}
