using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField]
    GameObject pausePanel;

    public void PauseFunction()
    {
        print("deu certo");
        pausePanel.SetActive(true);
        Time.timeScale = 0f; //isso faz com que o tempo do jogo n�o continue
    }

    public void ResumeFunction()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f; //isso faz com que o tempo do jogo ocorra normalmente
    }
}
