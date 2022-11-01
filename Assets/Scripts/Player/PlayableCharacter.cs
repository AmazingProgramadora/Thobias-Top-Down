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
    Rigidbody2D boxRdbd;
    FixedJoint2D boxJoint;
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
        if (generalInputs.Actions.Pause.triggered)
        {
            print("Yay");
            // Pause.PauseFunction(); //PEGAR ESSE COMPONENTE DO OUTRO SCRIPT
        }

        if (generalInputs.Actions.SwitchingCameras.triggered)
        {
            print("Switched player");
            ChangePlayer(ManagerPlayer.Instance.GetInactivePlayerIndex());
        }
        if (generalInputs.PlayableCharacterInputs.Grab.IsPressed())
        {
            MoveBox();
        }
        else if (generalInputs.PlayableCharacterInputs.Grab.WasReleasedThisFrame())
        {
            if (boxRdbd != null)
            {
                boxRdbd.velocity = Vector2.zero;
                boxRdbd.bodyType = RigidbodyType2D.Kinematic;
                boxJoint.enabled = false;
                boxJoint.connectedBody = null;
                boxRdbd = null;
                boxJoint = null;
            }
        }
    }

    void MoveBox()
    {
        if (boxRdbd != null)
        {
            boxRdbd.bodyType = RigidbodyType2D.Dynamic;
            boxJoint.enabled = true;
            boxJoint.connectedBody = rdbd;
        }
    }
    private void ChangePlayer(int playerIndex)
    {
        StartCoroutine(ChangePlayerCoroutine(ManagerPlayer.Instance.playerCharacters[ManagerPlayer.Instance.GetInactivePlayerIndex()].transform, playerIndex));
    }
    IEnumerator ChangePlayerCoroutine(Transform newTarget, int playerIndex)
    {
        ManagerPlayer.Instance.activePlayer = playerIndex;
        PlayableCharacter novoPlayerAtivo = ManagerPlayer.Instance.playerCharacters[ManagerPlayer.Instance.activePlayer];

        CinemachineVirtualCamera vCam = cam.GetComponent<CinemachineBrain>().ActiveVirtualCamera.VirtualCameraGameObject.GetComponent<CinemachineVirtualCamera>();
        vCam.enabled = false;

        cam.transform.position = newTarget.position;
        yield return null; //dá delay de 1 frame
        vCam.Follow = newTarget;
        vCam.LookAt = newTarget;

        vCam.enabled = true;

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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box") || collision.gameObject.CompareTag("Mirror"))
        {
            boxRdbd = collision.gameObject.GetComponent<Rigidbody2D>();
            boxJoint = collision.gameObject.GetComponent<FixedJoint2D>();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box") || collision.gameObject.CompareTag("Mirror") && !generalInputs.PlayableCharacterInputs.Grab.IsPressed())
        {
            boxRdbd.velocity = Vector2.zero;
            boxRdbd.bodyType = RigidbodyType2D.Kinematic;
            boxJoint.enabled = false;
            boxJoint.connectedBody = null;
            boxRdbd = null;
            boxJoint = null;
        }


    }
}