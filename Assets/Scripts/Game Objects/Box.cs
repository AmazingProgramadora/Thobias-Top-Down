using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && Input.GetKeyDown(KeyCode.Space))
        {
            GetComponent<FixedJoint2D>().enabled = true;
            GetComponent<FixedJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
        }
    }
}
