using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class PlayableCharacter : MonoBehaviour
{
    Camera cam;
    GeneralInputs generalInputs;
    Rigidbody2D rdbd;
    Vector2 movementInputs;
    public float movementSpeed;
    private void Awake()
    {
        generalInputs = new GeneralInputs();
        rdbd = GetComponent<Rigidbody2D>();
    }
    private void Start()
    {
        cam = Camera.main;
    }
    private void FixedUpdate()
    {
        movementInputs = generalInputs.PlayableCharacterInputs.Movement.ReadValue<Vector2>();
        rdbd.velocity = movementInputs * movementSpeed;
    }

    void Update()
    {
        if (generalInputs.Actions.SwitchingCameras.triggered)
        {
            print("A");
            ChangePlayer(ManagerPlayer.Instance.GetInactivePlayerIndex());
            //this.enabled = false;
        }
    }
    private void ChangePlayer(int playerIndex)
    {

        CinemachineBrain cinemachineBrain = cam.GetComponent<CinemachineBrain>();

       // cinemachineBrain.ActiveVirtualCamera.Follow = 
       // cinemachineBrain.ActiveVirtualCamera.LookAt =
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