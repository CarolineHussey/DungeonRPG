using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<FloatingText> floatingTexts = new List<FloatingText>();  //declares and initiates the list to store the text.

    private void Start()
    {

    }
    private void Update()//update every txt in the array every frame
    {
        foreach(FloatingText txt in floatingTexts)
            txt.UpdateFloatingText();
    }
    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        FloatingText fText = GetFloatingText();
        fText.txt.text = msg;
        fText.txt.fontSize = fontSize;
        fText.txt.color = color;
        fText.go.transform.position = Camera.main.WorldToScreenPoint(position); // takes world position (position) and transfers world coordinates to screen coordinates so that the text can be positioned in the UI
        fText.motion = motion; //transfer knowledge from manager to the gameobject
        fText.duration = duration;//transfer knowledge from manager to the gameobject
        fText.Show(); //takes everything in the fText and shows it on the screen
    }

    private FloatingText GetFloatingText()
    {
        //is there any text that we can use that is not currently active? 
        FloatingText txt = floatingTexts.Find(t => !t.active); //t is used to iterate through the array.  this line will iterate over the floatingTexts array until it finds an item (t) that is not active
        
        if (txt == null) //if we don't find something in the array that we can use, we have to create a new one, and add it to the list.  
        {
            txt = new FloatingText();
            txt.go = Instantiate(textPrefab); //create a new gameobject
            txt.go.transform.SetParent(textContainer.transform); 
            txt.txt = txt.go.GetComponent<Text>(); //assign txt to the text Component of the new object
            floatingTexts.Add(txt);//add it to the list
        }
        return txt; //if we find something we can use in the floatingTexts array, return it
    }
}
