using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{


    Door door;
    public GameObject thisDoor;
    AudioSource audioSource;

    void Awake()
    {
        door = thisDoor.GetComponent<Door>();
        audioSource = GetComponent<AudioSource>();
    }

    public GameObject GetDoor()
    {
        Hud_Manager.instance.GetCorrectSprite(gameObject.GetComponent<SpriteRenderer>().sprite);
        StartCoroutine(DestroyChave());
        return thisDoor;
    }
    IEnumerator DestroyChave()
    {
        audioSource.Play();
        yield return new WaitForSeconds(0.1f);
        Destroy(gameObject);
    }
}