using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSFX : MonoBehaviour
{
    GeneralInputs generalInputs;
    AudioSource audioSource;
    PlayableCharacter box;
    [SerializeField] GameObject boxScript;

    private void Awake()
    {
        box = boxScript.GetComponent<PlayableCharacter>();
        audioSource = GetComponent<AudioSource>();
        generalInputs = new GeneralInputs();
    }

    private void FixedUpdate()
    {
        if(generalInputs.PlayableCharacterInputs.Movement.IsPressed() && box.boxJoint != null)
        {
            print("A");
            audioSource.Play();
        }
        else
        {
            audioSource.Stop();
        }
    }
}
