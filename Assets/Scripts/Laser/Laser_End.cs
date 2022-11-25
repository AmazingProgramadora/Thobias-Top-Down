using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_End : MonoBehaviour
{
    [SerializeField] bool order = false;

    bool on;
    float timeToOff;
    float currentTimeOff;

    [SerializeField] GameObject this_Door;
    Door door;
    Animator anima;
    AudioSource audioSource;

    private void Start()
    {
        door = this_Door.GetComponent<Door>();
        timeToOff = 0.1f;
        anima = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

    }
    private void Update()
    {
        currentTimeOff += Time.deltaTime;
        if (currentTimeOff > timeToOff && on)
        {
            on = false;
            Deactivate();
        }
    }
    public void Activate()
    {
        currentTimeOff = Time.deltaTime;
        if (!on)
        {
            anima.SetBool("LaserEndAnimator", true);
            audioSource.Play();
            if (door != null && !order)
                door.OpenDoor();
            else
                door.CloseDoor();
            //Inserir aqui o que deveria acontecer quando acertar os lasers
            on = true;
        }
    }

    void Deactivate()
    {

        anima.SetBool("LaserEndAnimator", false);
        if(!order)
            door.CloseDoor();
        else
            door.OpenDoor();
    }

}
