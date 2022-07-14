using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class KeyManager : MonoBehaviour

{
    public static KeyManager instance;

    public int keysCollected = 0;
    public int numberOfKeys = 8;
    public Text keyCounter;
    public GameObject exitPortal;
    public GameObject torches;

    //public GameObject KeyCounter;
    public List<GameObject> keys = new List<GameObject>();
    private void Awake()
    {
        instance = this;
    }

}