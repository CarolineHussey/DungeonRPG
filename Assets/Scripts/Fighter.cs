using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    //public fields
    public int hitPoint = 10;
    public int maxHitPoint = 10;
    public float pushRecoverySpeed = 0.2f;

    //Immunity
    protected float immuneTime = 1.0f; //to ensure player cant spam hits, and also doesn;t die instantly!
    protected float lastImmune; 

    // Push
    protected Vector3 pushDirection;

    //all fighters can receive damge and KO

    protected virtual void ReceiveDamage(Damage damage)
    {
        if (Time.time - lastImmune > immuneTime) //if the time we last got hit is greater than the immune time (ie. checks that we are not immune)
        {
            lastImmune = Time.time; //sets the time of last immune to now
            hitPoint -= damage.damageAmount; //reduces hitpoints by the amount of damage dealt
            pushDirection = (transform.position - damage.origin).normalized * damage.pushForce; //direction that the fighter is pushed - have the vector normalised and multiplied by pushForce

            GameManager.instance.ShowText(damage.damageAmount.ToString(), 15, Color.red, transform.position, Vector3.zero, 0.05f);

            if (hitPoint < 0)
            {
                hitPoint = 0;
                KnockOut();
            }
        }
    }

    protected virtual void KnockOut()
    {

    }
}
