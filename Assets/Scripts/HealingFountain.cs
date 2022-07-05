using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingFountain : Collidable
{
    public int HealingAmount = 1; //how many hitpoints to heal per second
    private float healCooldown = 1.0f; 
    private float lastHeal; //how long since last heal

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name != "Player")
            return;
        //if time since last heal exceeds the heal cooldown time then heal the player
        if (Time.time - lastHeal > healCooldown)
        {
            lastHeal = Time.time; //sets the time of last heal to current time
            GameManager.instance.player.Heal(HealingAmount); //heal the player - increase hitpoints by healingAmount
        }
    }
}
