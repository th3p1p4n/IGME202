using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject arrow;  //reference set in Inspector to Arrow GameObject in Hierarchy

    //NOTE: if a Component will be used often in Update(), then it is best to define a variable to hold a reference to it since GetComponent<>() is relatively time-cosuming call

    private Arrow arrow_script; //for the script componenent of the Arrow GameObject

    // Start is called before the first frame update
    void Start()
    {
        arrow_script = arrow.GetComponent<Arrow>();  
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            arrow_script.released = true;
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            arrow_script.released = false;
        }

        //the initial speed V and the initial direction theta are set in Arrow.cs to 100 m/s and 45 degrees, respectively

        //NOTE: add controls here to change V and theta, + or -
        //by calling AdjustInitSpeed(int deltaV) and AdjustInitAngle(int deltaTheta) 
        //when UpArrow, DownArrow, LeftArrow, RightArrow are pressed
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            arrow_script.AdjustInitSpeed(1);
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            arrow_script.AdjustInitSpeed(-1);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            arrow_script.AdjustInitAngle(1);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            arrow_script.AdjustInitAngle(-1);
        }
    }
}