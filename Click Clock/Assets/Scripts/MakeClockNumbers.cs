using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeClockNumbers : MonoBehaviour
{
    public GameObject ClockNumber;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] clockNumbers = new GameObject[12];
        int angle = 90;
        float xPos = 0f;
        float yPos = 0f;
        float radius = 3.3f;
        Vector3 position;
        string numberString = "";

        for(int i = 0; i < clockNumbers.Length; i++)
        {
            angle -= 30;
            xPos = radius * Mathf.Cos(Mathf.Deg2Rad * angle);
            yPos = radius * Mathf.Sin(Mathf.Deg2Rad *angle);
            position = new Vector3(xPos, yPos, 0);
            clockNumbers[i] = Instantiate(ClockNumber, position, Quaternion.identity);

            numberString = (i + 1).ToString();
            clockNumbers[i].GetComponent<TextMesh>().text = numberString;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
