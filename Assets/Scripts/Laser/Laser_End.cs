using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_End : MonoBehaviour
{
    bool on;
    float timeToOff;
    float currentTimeOff;

    [SerializeField] GameObject this_Door;
    Door door;
    Animator anima;

    private void Start()
    {
        door = this_Door.GetComponent<Door>();
        timeToOff = 0.1f;
        anima = GetComponent<Animator>();

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
            if (door != null)
            door.OpenDoor();
            //Inserir aqui o que deveria acontecer quando acertar os lasers
            on = true;
        }
    }

    void Deactivate()
    {
        anima.SetBool("LaserEndAnimator", false);
        door.CloseDoor();
    }

}
