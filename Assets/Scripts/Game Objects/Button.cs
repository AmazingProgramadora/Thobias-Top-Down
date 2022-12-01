using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [NonSerialized] public bool enableButtonCollision;
    Animator anima;
    AudioSource audioSource;
    Door door;
    [SerializeField] GameObject thisDoor;
    GameObject currentBox;

    void Awake()
    {
        door = thisDoor.GetComponent<Door>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        anima = GetComponent<Animator>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Box") && currentBox == null)
        {
            currentBox = collision.gameObject;
            anima.SetBool("ButtonDownAnimator", true);
            audioSource.Play();

            if (door != null)
                door.OpenDoor();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Box") && collision.gameObject == currentBox)
        {
            currentBox = null;
            anima.SetBool("ButtonDownAnimator", false);

            if (door != null)
                door.CloseDoor();
        }
    }
}