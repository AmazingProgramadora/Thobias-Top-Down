using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDoor : MonoBehaviour
{
    Animator anima;
    Collider2D door_Collider;

    void Awake()
    {
        door_Collider = GetComponent<Collider2D>();
    }

    void Start()
    {
        anima = GetComponent<Animator>();
    }
    void Update()
    {

    }

    public void OpenDoor()
    {
        //Door Opens here
        anima.SetBool("IsOpen", true);
        door_Collider.enabled = false;
    }

    public void CloseDoor()
    {
        //Door closes here
        anima.SetBool("IsOpen", false);
        door_Collider.enabled = true;
    }
}