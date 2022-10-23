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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            anima.SetBool("ButtonDownAnimator", true);
            GetComponent<Collider2D>().enabled = false;
            door.buttonOpenDoor = true;
        }
    }
}
