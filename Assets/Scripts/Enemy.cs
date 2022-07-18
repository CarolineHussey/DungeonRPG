using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    //Experience 
    public int xpValue = 1;

    //Logic - 
    public float triggerLength = 1; //if the distance between the enemy & player is less than triggerlength, the enemy will start chasing the player
    public float chaseLength = 5; // sets the distance for how long the enemy will chase the player (will return to startpoint once the distance is greater than chaseLength

    private bool chasing; 
    private bool collideWithPlayer; 
    private Transform playerTransform;
    private Vector3 startingPosition;

    //Hitbox - replicates collider functionality found in collidable, but as we are already inheriting from Mover, we cannot also inherit from Collidable 
    public ContactFilter2D filter;
    private BoxCollider2D hitBox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        playerTransform = GameManager.instance.player.transform;
        //playerTransform = GameObject.Find("Player").transform; works in the same way as using GameManager to find
        startingPosition = transform.position; //the position of the enemies at the start of the game. 
        hitBox = transform.GetChild(0).GetComponent<BoxCollider2D>(); //use getChild(0) to get the child component at index one (ie. the hitbox attached to the enemy)
    }

    protected void FixedUpdate()
    {
        //if the player is in range - if not - do nothing. 
        if(Vector3.Distance(playerTransform.position, startingPosition) < chaseLength) //if player position and starting position is smaller than and chaseLength 
        {
            if (Vector3.Distance(playerTransform.position, startingPosition) < triggerLength)
            chasing = true;

            if (chasing)
            {
                if(!collideWithPlayer)
                {
                    UpdateMotor((playerTransform.position - transform.position).normalized); //the enemy will move in the direction of the player
                }
            }
            else
            {
                UpdateMotor(startingPosition - transform.position); //we go back to where we were
            }
                
        }

        //when the player is not in range
        else
        {
            UpdateMotor(startingPosition - transform.position); 
            chasing = false;
        }

        //check for overlaps
        collideWithPlayer = false;

        //copied from collidable script
        boxCollider.OverlapCollider(filter, hits); //OverlapCollider takes in the ContactFilter (the item collided with) and an array )to store the item collided with)
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i] == null)
                continue;

            if(hits[i].tag == "Fighter" && hits[i].name == "Player")
            {
                collideWithPlayer = true; 
            }

            hits[i] = null; //resets the array
        }

    }

    protected override void KnockOut()
    {
        Destroy(gameObject);
        GameManager.instance.GrantXp(xpValue);//when the enemy is defeated, call the grantXP function & grant the amount of XP attributed to xpValue
        GameManager.instance.ShowText("+" + xpValue + " xp", 30, Color.magenta, transform.position, Vector3.up * 40, 1.0f);
    }
}   
