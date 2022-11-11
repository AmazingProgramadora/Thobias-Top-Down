using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public bool pressed = false;
    [NonSerialized] public bool enableButtonCollision;
    Animator anima;
    Door door;
    [SerializeField] GameObject thisDoor;

    void Awake()
    {
        door = thisDoor.GetComponent<Door>();
    }

    void Start()
    {
        anima = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Box"))
        {
            anima.SetBool("ButtonDownAnimator", true);
            pressed = true;

            if (door != null)
                door.OpenDoor();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Box"))
        {

            anima.SetBool("ButtonDownAnimator", false);
            pressed = false;

            if (door != null)
                door.CloseDoor();
        }
    }
}