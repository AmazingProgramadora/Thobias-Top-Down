using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [NonSerialized] public bool keyOpenDoor, buttonOpenDoor;

    public bool checkForKey;
    public bool checkForButton;

    Button button;
    public GameObject whichButton;
    Animator anima;

    void Awake()
    {
        if (whichButton != null)
            button = whichButton.GetComponent<Button>();
    }
    void Update()
    {

    }
    void Start()
    {
        anima = GetComponent<Animator>();
    }

    public void OpenDoor()
    {
        print("A");
    }
}