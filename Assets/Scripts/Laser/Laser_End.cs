using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_End : MonoBehaviour
{
    bool on;
    float timeToOff;
    float currentTimeOff;

    public GameObject door;

    private void Start()
    {
        timeToOff = 0.1f;
    }
    private void Update()
    {
        currentTimeOff += Time.deltaTime;
        if(currentTimeOff>timeToOff)
            on = false;
    }
    public void Activate()
    {
        currentTimeOff = Time.deltaTime;
        if (!on)
        {
            if(door != null)
            door.GetComponent<Door>().OpenDoor();
            //Inserir aqui o que deveria acontecer quando acertar os lasers
            on = true;
        }
    }

}
