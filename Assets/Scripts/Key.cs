using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Key : Collectable
{
    
    
    public Text keyCounter;
    public GameObject exitPortal;
    public GameObject torches;
    protected override void OnCollect()
    {

        if (!collected)
        {
            base.OnCollect();
            KeyManager.instance.keysCollected++;
            GameManager.instance.ShowText("+ " + KeyManager.instance.keysCollected + " key collected", 25, Color.cyan, transform.position, Vector3.up * 25, 1.5f);
            Destroy(gameObject);
            Debug.Log(KeyManager.instance.keysCollected + " collected!");
            keyCounter.text = "Keys: " + KeyManager.instance.keysCollected + " / " + KeyManager.instance.numberOfKeys;
        }

        if (KeyManager.instance.keysCollected >= KeyManager.instance.numberOfKeys)
        {
            Debug.Log("All keys are collected");
            GameManager.instance.ShowText("A new portal has opened!", 25, Color.green, transform.position + Vector3.down, Vector3.zero, 1.5f);
            exitPortal.SetActive(true);
            torches.SetActive(true);
        }   



    }

}
