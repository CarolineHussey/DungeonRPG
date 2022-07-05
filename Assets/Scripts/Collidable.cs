using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    public ContactFilter2D filter; //a filter to know what exacly is collided with
    private BoxCollider2D boxCollider;
    private Collider2D[] hits = new Collider2D[10]; //to contain data of what we hit during the frame

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>(); //Creates a box collider for the object that the script is attached to
    }

    protected virtual void Update()
    {
        //Collison Work
        boxCollider.OverlapCollider(filter, hits); //OverlapCollider takes in the ContactFilter (the item collided with) and an array (to store the item collided with)
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            OnCollide(hits[i]); //call this function to see that was collided with in the console log

            hits[i] = null; //resets the array
        }
    }

    protected virtual void OnCollide(Collider2D coll) //the function creates a versatile debug - any Collider2D object can be passed to it
    {
        Debug.Log("OnCollide was not implemented in " + this.name); //string added here to distinguise debug.log that runs here from the overrise function in weapon.cs.  
    }
}
