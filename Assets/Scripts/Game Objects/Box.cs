using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Box : MonoBehaviour
{
    #region Declarations
    GeneralInputs generalInputs;

    #endregion
    void Start()
    {
        generalInputs = new GeneralInputs();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player") && generalInputs.PlayableCharacterInputs.Grab.triggered)
        {
            GetComponent<FixedJoint2D>().enabled = true;
            GetComponent<FixedJoint2D>().connectedBody = GetComponent<Rigidbody2D>();
        }
    }
}
