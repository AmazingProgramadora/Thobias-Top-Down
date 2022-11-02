using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    [SerializeField]
    AudioClip interactSound;
    GeneralInputs generalInputs;
    AudioSource audioSource;

    private void Awake()
    {
        generalInputs = new GeneralInputs();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (Time.timeScale == 1f && generalInputs.PlayableCharacterInputs.Movement.IsPressed())
        {
             if (audioSource.isPlaying == false)
                 audioSource.Play();
        }
        else
        {
            audioSource.Stop(); //entender pq nao esta parando quando o player esta andando e da pausa
        }

        if (generalInputs.PlayableCharacterInputs.Interact.triggered)
        {
            print("yay");
            audioSource.PlayOneShot(interactSound);
        }
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
