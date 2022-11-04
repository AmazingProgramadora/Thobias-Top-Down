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

    private void Start()
    {
        door = this_Door.GetComponent<Door>();
        timeToOff = 0.1f;
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
            if(door != null)
            door.OpenDoor();
            //Inserir aqui o que deveria acontecer quando acertar os lasers
            on = true;
        }
    }

    void Deactivate()
    {
        door.CloseDoor();
    }

}
