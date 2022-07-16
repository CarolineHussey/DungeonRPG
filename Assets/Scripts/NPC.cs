using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Collidable
{
    public string message;
    private float cooldown = 2.0f;
    private float lastText;

    protected override void Start()
    {
        base.Start();
        lastText = -cooldown; //by setting this to - cooldown to start with, we won't have to wait for the cooldown time at the start of our interaction with the NPC
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name != "Player")
            return;

        if (Time.time - lastText > cooldown)
        {
            lastText = Time.time;
            GameManager.instance.ShowText(message, 30, Color.yellow, transform.position + Vector3.down, Vector3.zero, cooldown);
        }
    }
}
