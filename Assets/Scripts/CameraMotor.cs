using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    private Transform lookAt; //the comparison view - to compare the camera view to (eg. where the player is in relation to the camera)
    public float boundX = 0.15f; //how far the player can go in x axis before the camera starts following them. 
    public float boundY = 0.05f; //how far the player can go in x axis before the camera starts following them. 

    private void Start()
    {
        lookAt = GameObject.Find("Player").transform;
    }

    //Late update is called after update and FixedUpdate - so these actions will occur after any that are set in those earlier updates. 
    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero; // the difference bwtween this frame & next
        //checks if we are inside the bounds on the X Axis
        float deltaX = lookAt.position.x - transform.position.x; //  DeltaX checks for the difference between the players location & the camera on the x axis.  transform = centre point of camera.
        if (deltaX > boundX || deltaX < -boundX) //checks if deltaX is outside of the bounds on the right or the left
        {
            if(transform.position.x < lookAt.position.x) //is the centre of the camera less than the player.  
            {
                delta.x = deltaX - boundX; //if yes, then the player is on the right and the camera is on the left - so we have to add to the delta position. 
            }
            else
            {
                delta.x = deltaX + boundX; //otherwise, the player is on the other sde - so we use + boundX
            }
        }
        //checks if we are inside the bounds on the Y Axis
        float deltaY = lookAt.position.y - transform.position.y; 
        if (deltaY > boundY || deltaY < -boundY) 
        {
            if (transform.position.y < lookAt.position.y) 
            {
                delta.y = deltaY - boundY;
            }
            else
            {
                delta.y = deltaY + boundY;
            }
        }
        transform.position += new Vector3(delta.x, delta.y, 0); //z axis ahould always be set to 0!
    }
}
