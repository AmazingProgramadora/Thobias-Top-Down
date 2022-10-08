using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayableCharacter : MonoBehaviour
{
    GeneralInputs generalInputs;
    Rigidbody2D rdbd;
    Vector2 movementInputs;
    public float movementSpeed;
    private void Awake()
    {
        generalInputs = new GeneralInputs();
        rdbd = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        movementInputs = generalInputs.PlayableCharacterInputs.Movement.ReadValue<Vector2>();
        rdbd.velocity = movementInputs * movementSpeed;
    }

    void Update()
    {

    }

    private void OnEnable()
    {
        generalInputs.Enable();
    }

    private void OnDisable()
    {
        generalInputs.Disable();
    }
}