using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    void Awake()
    {

    }
    void Update()
    {

    }
    void Start()
    {

    }

    public void OpenDoor()
    {
        Destroy(gameObject);
    }
}