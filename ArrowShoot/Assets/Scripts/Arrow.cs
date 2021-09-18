using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
   
   /*

   I shot an arrow into the air, 
   It fell to earth, I knew not where; 
   For, so swiftly it flew, the sight 
   Could not follow it in its flight.

   from The Arrow and the Song
   by Henry Wadsworth Longfellow

   */

    private int v = 100; //magnitude of the initial velocity vector, i.e. initial speed
    private int theta; // initial direction of the velocity vector
    private const float initialHeight = 2f; //initial height
    private const float g = 9.8f; //this is in units of meters per second per second

    float tT;  //this will keep track of the total elapsed time

    Vector3 acc;
    Vector3 vel;
    Vector3 pos;
   
    public bool released; //this is made public so as to be accessible to GameManager

    public GameObject plotRenderer;  //must be set in Inspector

    void Start()
    {
        released = false;

        tT = 0f;

        acc = new Vector3(0f, -g, 0f); //this vector is constant in magnitude and direction (always pointing straight down)

        v = 100;  //initial speed = initial magnitude of velocity

        theta = 45;  //initial angle of velocity

        pos = new Vector3(0, initialHeight, 0); //NOTE: transform.position.y cannot be set to the initial height, it is read-only

        transform.position = pos;  //initial position

        TakeAim();   //call TakeAim() after any change in initial position or velocity

    }

    void Update()
    {
        if (released)
        {
            tT += Time.deltaTime;  //total elapsed time

            //update vel and pos, computed on a (time)step-by-step basis using "Euler integration method", which is simply a linear approximation scheme

            vel = vel + Time.deltaTime * acc;  //since acc is constant, this produces an exact (linear) solution for vel
            pos = pos + Time.deltaTime * vel; //this is a good approximation to pos (a quadratic) since vel is linear

        
            //uncomment these to fly arrow using "exact solution" (same as plotter) instead of the approximate solution computed above
            /*
            vel.x = v * Mathf.Cos(theta * Mathf.Deg2Rad);
            vel.y = v * Mathf.Sin(theta * Mathf.Deg2Rad) - g * (tT);

            //here we have point (x0, y0) of initial position as x0 = 0 and y0 = initialHeight
            pos.x =  v * Mathf.Cos(theta * Mathf.Deg2Rad) * (tT);
            pos.y = initialHeight + v * Mathf.Sin(theta * Mathf.Deg2Rad) * (tT) - 0.5f * g * (tT) * (tT);
            */

            transform.position = pos;

            //update the orientation of the arrow so that it points in the direction of the velocity vector
            if (vel.sqrMagnitude > 0)
            {
                transform.right = vel.normalized; //NOTE: in 2D (in the x-y plane, with transform.forward fixed) the transform.right vector will rotate when transform.up does, and vice-versa
                //and for the sake of comparison, here are two alternatives: one correct, the other one incorrect, respectively
                //transform.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(vel.y, vel.x)));
                //transform.Rotate(new Vector3(0, 0, Mathf.Rad2Deg * Mathf.Atan2(vel.y, vel.x)));  //NOTE:  despite its similarity to the above, THIS METHOD DOESN'T WORK!
            }
        }
    }

    public void TakeAim()
    {
        vel = v * (new Vector3(Mathf.Cos(theta * Mathf.Deg2Rad), Mathf.Sin(theta * Mathf.Deg2Rad), 0f));  //the initial velocity upon release of arrow

        transform.right = vel.normalized;   //NOTE:  if the arrow image was oriented "up", then the statement would need to be: transform.up = vel.normalized

        plotRenderer.GetComponent<Plotter>().PlotTrajectory(pos, vel);

        //for reference purposes; this is the formula of the exact solution for the trajectory of a projectile, as used by Plotter
        // (x0, y0) is the initial position, v * < cos(theta), sin(theta) > is the initial velocity
        // (x(t), y(t)) is the position of the projectile at time t 
        /*
        x(t) = x0 + v * Mathf.Cos(theta) * (tT);
        y(t) = y0 + v * Mathf.Sin(theta) * (tT) - 0.5f * g * (tT) * (tT);
        */
    }

    public void AdjustInitAngle(int deltaTheta)
    {
        if((theta < 90 && theta > 0) || (theta >= 90 && deltaTheta <= 0) || (theta <= 0 && deltaTheta >= 0))
        {
            theta += deltaTheta;

            //should call TakeAim() after any change in initial position or velocity
            TakeAim();
        }
    }

    public void AdjustInitSpeed(int deltaV)
    {
        if(v > 0 || (v <= 0 && deltaV >= 0))
        {
            v += deltaV;

            //should call TakeAim() after any change in initial position or velocity
            TakeAim();
        }
    }


    void OnGUI()
    {
        int time, speed, direction, distance;
        GUI.color = Color.white;
        GUI.skin.box.fontSize = 15;
        GUI.skin.box.wordWrap = false;

        //note:  must use (int) or else the float digits flicker

        speed = (int)vel.magnitude;

        time = (int)tT;
        
        direction = (int)Vector3.SignedAngle(Vector3.right, vel, Vector3.forward);

        distance = (int)(speed*time);


        GUI.Box(new Rect(5, 5, 100, 30), "speed = " + speed);

        GUI.Box(new Rect(5, 35, 100, 30), "time = " + time);

        GUI.Box(new Rect(5, 65, 100, 30), "direction = " + direction);

        GUI.Box(new Rect(5, 95, 100, 30), "distance = " + distance);
    }


}
