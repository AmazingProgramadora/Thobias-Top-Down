using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using System.Security.Cryptography;

public class PlayableCharacter : MonoBehaviour
{
    bool pushed = true;
    Camera cam;
    GeneralInputs generalInputs;
    Rigidbody2D rdbd;
    Animator anima;
    Vector2 movementInputs;
    public float movementSpeed;
    Rigidbody2D boxRdbd;
    public FixedJoint2D boxJoint;
    AudioSource audioSource;
    [SerializeField] AudioClip BoxSFX;
    [SerializeField]
    AudioClip interactSound;
    //public int maxHelath = 100;
    //public int currentHealth;
   // public HealthBar healthBar;
    private void Awake()
    {
        generalInputs = new GeneralInputs();
        rdbd = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        //currentHealth = maxHelath;
        //healthBar.SetMaxHealth(maxHelath);

        anima = GetComponent<Animator>();
        cam = Camera.main;
        if (!ManagerPlayer.Instance.playerCharacters[ManagerPlayer.Instance.activePlayer].Equals(this))
        {
            enabled = false;
        }
    }
    private void FixedUpdate()
    {
        movementInputs = generalInputs.PlayableCharacterInputs.Movement.ReadValue<Vector2>();
        
        //Checa se esta movendo para o animator

        //provavelmente nao eh a maneira mais eficaz
        if(movementInputs != Vector2.zero && anima.GetBool("IsPushing") == false)
        {
            anima.SetFloat("Xinput", movementInputs.x);
            anima.SetFloat("Yinput", movementInputs.y);

            anima.SetBool("IsWalking", true);
        }
        else if(movementInputs != Vector2.zero && anima.GetBool("IsPushing") == true)
        {
            anima.SetBool("IsWalking", true);
            StartCoroutine(SFX());

        }
        else
        {
            anima.SetBool("IsWalking", false);
        }
        rdbd.velocity = movementInputs * movementSpeed;
    }

    void Update()
    {
        if (generalInputs.PlayableCharacterInputs.Interact.triggered)
        {
            audioSource.PlayOneShot(interactSound);
        }

            if (generalInputs.Actions.SwitchingCameras.triggered)
        {
            ChangePlayer(ManagerPlayer.Instance.GetInactivePlayerIndex());
        }

        //checa se o player esta segurando caixa
        if (generalInputs.PlayableCharacterInputs.Grab.IsPressed())
        {
            anima.SetBool("IsPushing", true);
            MoveBox();
        }
        else if (generalInputs.PlayableCharacterInputs.Grab.WasReleasedThisFrame())
        {
            anima.SetBool("IsPushing", false);

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

        /*if (generalInputs.PlayableCharacterInputs.TakeDamage.WasReleasedThisFrame())
        {
            TakeDamage(20);
        }*/
    }

    /*public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }*/

    IEnumerator SFX()
    {
        if (pushed == true)
        {
            audioSource.PlayOneShot(BoxSFX);
            pushed = false;
            yield return new WaitForSeconds(0.561F);
            pushed = true;
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
        if (collision.gameObject.CompareTag("Box"))
        {
            boxRdbd = collision.gameObject.GetComponent<Rigidbody2D>();
            boxJoint = collision.gameObject.GetComponent<FixedJoint2D>(); //Crime contra a humanidade
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box") || collision.gameObject.CompareTag("Mirror") && !generalInputs.PlayableCharacterInputs.Grab.IsPressed())
        {
            if (boxRdbd != null && boxJoint != null)
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
}