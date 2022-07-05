using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//making this class abstract means that it has to be inherited from or else it just won't work. 
public abstract class Mover : Fighter 
{
    private Vector3 originalSize;
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit; //for casting the players collider box where they should be in the future. 
    public float ySpeed = 0.75f;
    public float xSpeed = 1.0f;
    protected virtual void Start()
    {
        originalSize = transform.localScale;
        boxCollider = GetComponent<BoxCollider2D>();

    }

    //to differentiate between the NPC and player, we will have the NPC will have pre-set actions that it will respond to and the player will respond to keys
    protected virtual void UpdateMotor(Vector3 input) 
    {
        //reset moveDelta
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);

        //swap the sprite direction based on wether it is moving left or right
        if (moveDelta.x > 0)
            transform.localScale = originalSize;
        else if (moveDelta.x < 0)
            transform.localScale = new Vector3(originalSize.x * -1, originalSize.y, originalSize.z);
        // Add push vector
        moveDelta += pushDirection; // pushirection is inherited from Fighter
        // reduce push force every frame.  reduction based on recovery factor of both enemy & fighter, from Fighter script.  This will limit how much the mover is pushed.   
        pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);

        //Boxcast(current_position, box_collider_size, player_angle,  future_direction  , distance, the_applicable_layers) - Casts a box in the specified diection.  if the boxcast hits something, we can't go there.  If it doesn;t hit anything, we can move!
        //future_direction is moved one axis at a time in case that's what is required (y axis = up & down) 
        //distance to match what is in translate below)
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(0, moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null) // checks if the move is permitted.  If move is permitted - proceed;
        {
            //make the sprite move!
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0); //this sets the movement on the x axis to 0 (no change) and y axis is deltatime.  
        }

        //same boxCast but for the x axis
        hit = Physics2D.BoxCast(transform.position, boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if (hit.collider == null) // checks if the move is permitted.  If move is permitted - proceed;
        {
            //make the sprite move!
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0); //this sets the movement on the x axis to 0 (no change) and y axis is deltatime.  
        }
    }
}
