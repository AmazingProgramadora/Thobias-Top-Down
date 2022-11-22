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

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {

            audioSource.Play();
            door.OpenDoor();
            Destroy(gameObject);
        }
    }*/

    public GameObject GetDoor()
    {
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