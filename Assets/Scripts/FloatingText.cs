using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //this imports the 'Text' object 

public class FloatingText
{
    public bool active;
    public GameObject go; //reference to the GameObject
    public Text txt;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    public void Show() //call this when we decide to show the text
    {
        active = true;
        lastShown = Time.time; //right now
        go.SetActive(active);
    }

    public void Hide()
    {
        active = false;
        go.SetActive(active);
    }

    public void UpdateFloatingText()
    {
        if (!active)
            return; //do nothing

        // if (time now - time started showing) > duration of text
        if (Time.time - lastShown > duration)
            Hide();

        go.transform.position += motion * Time.deltaTime; 
    }
}
