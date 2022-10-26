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
    public float distance = 1f;
    public LayerMask boxMask;
    GameObject box;
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
        Physics2D.queriesStartInColliders = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, distance, boxMask);

        if (hit.collider != null && Input.GetKeyDown (KeyCode.Space))
        {
            box = hit.collider.gameObject;

            box.GetComponent<FixedJoint2D>().enabled = true;
            box.GetComponent<Box>().beingPushed = true;
            box.GetComponent<FixedJoint2D>().connectedBody = this.GetComponent<Rigidbody2D>();
        }

        else if (Input.GetKeyUp (KeyCode.Space))
        {
            box.GetComponent<FixedJoint2D>().enabled = false;
            box.GetComponent<Box>().beingPushed = false;
        }
        
        if (generalInputs.Actions.SwitchingCameras.triggered)
        {
            print("A");
            ChangePlayer(ManagerPlayer.Instance.GetInactivePlayerIndex());
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawLine(transform.position, (Vector2)transform.position + Vector2.right * transform.localScale.x * distance);
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
}