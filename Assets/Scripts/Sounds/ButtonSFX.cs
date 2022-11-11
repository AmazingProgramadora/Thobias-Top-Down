using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSFX : MonoBehaviour
{
    AudioSource audioSource;
    Button button;
    [SerializeField] GameObject buttonScript;

    private void Awake()
    {
        button = buttonScript.GetComponent<Button>();
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (button.pressed == true)
        {
            audioSource.Play();
        }
    }



}