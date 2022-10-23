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
        OpenDoor();
    }
    void Start()
    {
        anima = GetComponent<Animator>();
    }


    public void OpenDoor()
    {
        if (checkForKey && keyOpenDoor)
        {
            if (checkForButton)
            {
                button.enableButtonCollision = true;
            }
            else if (checkForButton == false)
                StartCoroutine(OpenTheSesame());
        }

        if (checkForKey == false)
        {
            if (checkForButton)
            {
                button.enableButtonCollision = true;
            }
            else if (checkForButton == false)
            {
                StartCoroutine(OpenTheSesame());
            }
        }

        if (checkForKey && keyOpenDoor == false)
        {
            if (checkForButton)
            {
                button.enableButtonCollision = true;
            }
            else if (checkForButton == false)
                StartCoroutine(OpenTheSesame());
        }

        if (checkForButton && checkForKey == false)
        {
            button.enableButtonCollision = true;
        }

        if (whichButton != null)
        {
            if (button.enableButtonCollision && button.GetComponent<Animator>().GetBool("ButtonDownAnimator"))
                buttonOpenDoor = true;
        }

        if (buttonOpenDoor == true)
            StartCoroutine(OpenTheSesame());

    }

    public IEnumerator OpenTheSesame()
    {
        anima.SetBool("DoorOpenAnimator", true);
        gameObject.GetComponent<Collider2D>().enabled = false;
        yield return new WaitForSeconds(0.5f);
    }
}
