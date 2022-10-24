using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [NonSerialized] public bool enableButtonCollision;
    Animator anima;
    Door door;
    public GameObject thisDoor;

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
        if (collision.CompareTag("Player"))
        {
            anima.SetBool("ButtonDownAnimator", true);
            GetComponent<Collider2D>().enabled = false;
            door.OpenDoor();
        }
    }
}