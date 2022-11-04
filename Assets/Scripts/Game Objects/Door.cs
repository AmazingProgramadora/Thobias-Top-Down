using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    Collider2D door_Collider;

    void Awake()
    {
        door_Collider = GetComponent<Collider2D>();
    }
    void Update()
    {

    }
    void Start()
    {

    }

    public void OpenDoor()
    {
        //Door Opens here
        door_Collider.enabled = false;
    }

    public void CloseDoor()
    {
        //Door closes here
        door_Collider.enabled = true;
    }
}