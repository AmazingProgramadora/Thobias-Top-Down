using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    Animator anima;
    Collider2D door_Collider;
    CircleCollider2D circle_Collider;

    void Awake()
    {
        circle_Collider = GetComponent<CircleCollider2D>();
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
        if(circle_Collider != null) 
            circle_Collider.enabled = false;
    }

    public void CloseDoor()
    {
        //Door closes here
        anima.SetBool("IsOpen", false);
        door_Collider.enabled = true;
        if (circle_Collider != null)
            circle_Collider.enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (GameObject door in ManagerPlayer.Instance.keyList)
            {
                if (door == gameObject)
                {
                    OpenDoor();                    
                    break;
                }

            }
        }
    }
}