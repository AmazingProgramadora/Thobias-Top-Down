using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using System.Security.Cryptography;
using UnityEditor.PackageManager;

public class PlayableCharacter : MonoBehaviour
{


    #region Variables
    [SerializeField]
    float rangeDistanceGrab, rangeHeightGrab;
    [SerializeField]
    LayerMask layerMask;
    bool pushed = true;
    Camera cam;
    Animator animator;
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
    Vector2 lastInput;
    private bool isGrabbing;
    //public int maxHelath = 100;
    //public int currentHealth;
    // public HealthBar healthBar;
    #endregion

    #region Lists


    #endregion

    #region Updates/Start/Awake
    private void Awake()
    {
        gameObject.GetComponent<Rigidbody2D>().WakeUp();

        generalInputs = new GeneralInputs();
        rdbd = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        //currentHealth = maxHelath;
        //healthBar.SetMaxHealth(maxHelath);

        anima = GetComponent<Animator>();
        cam = Camera.main;
        //if (!ManagerPlayer.Instance.playerCharacters[ManagerPlayer.Instance.activePlayer].Equals(this))
        //{
        //    enabled = false;
        //}
    }
    private void FixedUpdate()
    {
        movementInputs = generalInputs.PlayableCharacterInputs.Movement.ReadValue<Vector2>();

        //Checa se esta movendo para o animator

        //provavelmente nao eh a maneira mais eficaz
        if (movementInputs != Vector2.zero && anima.GetBool("IsPushing") == false)
        {
            anima.SetFloat("Xinput", movementInputs.x);
            anima.SetFloat("Yinput", movementInputs.y);

            anima.SetBool("IsWalking", true);

            lastInput = new Vector2(movementInputs.x, movementInputs.y);
        }
        else if (movementInputs != Vector2.zero && anima.GetBool("IsPushing") == true)
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
        //if (generalInputs.PlayableCharacterInputs.Interact.triggered)
        //{
        //    audioSource.PlayOneShot(interactSound);
        //}

        /*if (generalInputs.Actions.SwitchingCameras.triggered)
        {
            ChangePlayer(ManagerPlayer.Instance.GetInactivePlayerIndex());
        }*/

        RangeGrab();

        /*if (generalInputs.PlayableCharacterInputs.TakeDamage.WasReleasedThisFrame())
        {
            TakeDamage(20);
        }*/
    }
    #endregion

    /*public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }*/

    #region Sound
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
    #endregion

    #region Camera
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

        //essa parte conserta o bug de continuar andando quando troca de cameras
        int notPlayerIndex;
        if (playerIndex == 0)
            notPlayerIndex = 1;
        else
            notPlayerIndex = 0;
        ManagerPlayer.Instance.playerCharacters[notPlayerIndex].GetComponent<Rigidbody2D>().Sleep();
    }
    #endregion

    #region Enables
    private void OnEnable()
    {
        generalInputs.Enable();
    }

    private void OnDisable()
    {
        generalInputs.Disable();
    }
    #endregion

    #region Collisions
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Box") && !isGrabbing)
        {
            boxRdbd = collision.gameObject.GetComponent<Rigidbody2D>();
            boxJoint = collision.gameObject.GetComponent<FixedJoint2D>(); //Crime contra a humanidade
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //print(collision.gameObject.name);
        if (collision.gameObject.CompareTag("Box") || collision.gameObject.CompareTag("Mirror"))
        {
            if (boxRdbd != null && boxJoint != null && !isGrabbing)
            {
                boxRdbd = null;
                boxJoint = null;
            }   
        }       
    }           

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Key"))
        {
            ManagerPlayer.Instance.keyList.Add(collision.gameObject.GetComponent<Key>().GetDoor());
            Hud_Manager.instance.CheckKeys();
        }
        if (collision.CompareTag("TimeTransition"))
        {
            ChangePlayer(ManagerPlayer.Instance.GetInactivePlayerIndex());
        }
    }
    #endregion

    #region Grab
    void MoveBox()
    {
        if (boxRdbd != null && !isGrabbing)
        {
            isGrabbing = true;
            boxRdbd.bodyType = RigidbodyType2D.Dynamic;
            boxJoint.enabled = true;
            boxJoint.connectedBody = rdbd;
        }
    }
    private void GrabFunction()  //checa se o player esta segurando caixa
    {
        if (generalInputs.PlayableCharacterInputs.Grab.IsPressed())
        {
            anima.SetBool("IsPushing", true);
            MoveBox();
        }

        if (!generalInputs.PlayableCharacterInputs.Grab.IsPressed())
        {
            anima.SetBool("IsPushing", false);

            if (boxRdbd != null && generalInputs.PlayableCharacterInputs.Grab.WasReleasedThisFrame())
            //o problema do WasReleasedThisFrame() eh que se a acao nao acontece naquele frame, a informacao nao eh gravada, tem que consertar isso (???)
            {
                boxRdbd.velocity = Vector2.zero;
                boxRdbd.bodyType = RigidbodyType2D.Kinematic;
                boxJoint.enabled = false;
                boxJoint.connectedBody = null;
                isGrabbing = false;               
            }
        }
    }

    private void RangeGrab()
    {
        Vector3 heightAdjustment = new Vector3(rangeDistanceGrab / 7, rangeHeightGrab, 0);
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lastInput, rangeDistanceGrab / 7, layerMask);

        if (hit.collider != null)
        {
            Debug.DrawLine(transform.position, hit.point);

            if (hit.collider.gameObject.CompareTag("Box") || hit.collider.gameObject.CompareTag("Mirror"))
                GrabFunction();

            else
                animator.SetBool("IsPushing", false);
        }

        if (hit.collider == null)
        {
            animator.SetBool("IsPushing", false);
        }

    }

    #endregion




}