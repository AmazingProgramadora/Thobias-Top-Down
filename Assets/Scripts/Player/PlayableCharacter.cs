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
        if (!ManagerPlayer.Instance.playerCharacters[ManagerPlayer.Instance.activePlayer].Equals(this))
        {
            enabled = false;
        }
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
        }
    }
    private void ChangePlayer(int playerIndex)
    {
        ManagerPlayer.Instance.activePlayer = playerIndex;
        PlayableCharacter novoPlayerAtivo = ManagerPlayer.Instance.playerCharacters[ManagerPlayer.Instance.activePlayer];

        CinemachineBrain cinemachineBrain = cam.GetComponent<CinemachineBrain>();
        cinemachineBrain.ActiveVirtualCamera.Follow = null;
        cinemachineBrain.ActiveVirtualCamera.LookAt = null;     
        cam.transform.position = novoPlayerAtivo.transform.position;
    

        cinemachineBrain.ActiveVirtualCamera.Follow = ManagerPlayer.Instance.playerCharacters[playerIndex].transform;
        cinemachineBrain.ActiveVirtualCamera.LookAt = ManagerPlayer.Instance.playerCharacters[playerIndex].transform;

        novoPlayerAtivo.enabled = true;
        enabled = false;
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