using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Potion : Collidable
{
    public int HealAmount = 1;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name != "Player")
            return;

        else
        {
            GameManager.instance.player.Heal(HealAmount); //heal the player - increase hitpoints by healingAmount
            Destroy(gameObject);
        }
    }
}