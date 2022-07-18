using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldKey : Collectable
{
    protected override void OnCollect()
    {

        if (!collected)
        {
            base.OnCollect();
            KeyManager.instance.goldKey = true;
            GameManager.instance.ShowText("You found a strange key", 30, Color.cyan, transform.position, Vector3.up * 25, 1.5f);
            Destroy(gameObject);
        }

        if (KeyManager.instance.goldKey)
        {
            KeyManager.instance.npc.SetActive(true);
        }



    }
}
