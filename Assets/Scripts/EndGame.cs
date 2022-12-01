using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndGame : MonoBehaviour
{
    public static bool endTrigger = false;
    public static EndGame instance;

    private void Awake()
    {
        instance = this;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            endTrigger = true;
            SceneManager.LoadScene(0);//evite colocar string, coloque o índice (número) da Build
        }

    }
}
