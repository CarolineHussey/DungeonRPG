using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//this script can be added to the enemy or enemy weapon to 'fight' our player
public class EnemyHitBox : Collidable
{
    //damage
    public int damagePt = 1;
    public float pushForce = 5;

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.tag == "Fighter" && coll.name == "Player")
        {
            //create a new damage object before sending it to the fighter / player

            Damage damage = new Damage
            {
                damageAmount = damagePt,
                origin = transform.position,
                pushForce = pushForce
            };

            coll.SendMessage("ReceiveDamage", damage);
        }
    }
}
