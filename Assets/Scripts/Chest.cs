using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{

    public Sprite emptyChest;
    public int coinsAmount = 5;

    protected override void OnCollect()
    {
        if (!collected)
        {
            base.OnCollect(); //collected = true by reference
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            GameManager.instance.coins += coinsAmount;
            GameManager.instance.ShowText("+ " + coinsAmount + " coins", 25, Color.white, transform.position, Vector3.up * 25, 1.5f); //every second the Floating Text Object will gain 50px in height; display for 3 seconds
            //Debug.Log("Grant " + coinsAmount + " coins!");
        }
        
        //Debug.Log("Grant coins");
    }
}
