using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField]
    GameObject pausePanel, optionsPanel, volumePanel;
    GeneralInputs generalInputs;

    private void Awake()
    {
        generalInputs = new GeneralInputs();
    }

    void Update()
    {
        if (generalInputs.Actions.Pause.triggered && pausePanel.activeSelf == false && Time.timeScale == 1f)
        {
            PauseFunction();
        }

        else if (generalInputs.Actions.Pause.triggered && pausePanel.activeSelf == true)
        {
            ResumeFunction();
        }

        StartCoroutine(Debounce());
    }

    public void PauseFunction()
    {
        print("Pause");
        pausePanel.SetActive(true);
        Time.timeScale = 0f; //isso faz com que o tempo do jogo não continue
    }

    public void ResumeFunction()
    {
        print("Resume");
        pausePanel.SetActive(false);
        Time.timeScale = 1f; //isso faz com que o tempo do jogo ocorra normalmente
    }
    public void OptionsFunction()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void OptionsBackFunction()
    {
        pausePanel.SetActive(true);
        optionsPanel.SetActive(false);
    }
    public void VolumeFunction()
    {
        optionsPanel.SetActive(false);
        volumePanel.SetActive(true);
    }

    public void VolumeBackFunction()
    {
        optionsPanel.SetActive(true);
        volumePanel.SetActive(false);
    }

    private void OnEnable()
    {
        generalInputs.Enable();
    }

    private void OnDisable()
    {
        generalInputs.Disable();
    }

    private IEnumerator Debounce()
    {
        if (generalInputs.Actions.Pause.triggered)
        { 
            yield return new WaitForSeconds(0.3f); //duracao do debounce
        }
    }
}
