using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    [SerializeField]
    GameObject pausePanel, goToPanel, actualPanel, endingCreditsPanel, menuPanel;
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

        if (EndGame.endTrigger && menuPanel!=null)
        {
            menuPanel.SetActive(false);
            endingCreditsPanel.SetActive(true);
            EndGame.endTrigger = false;
        
        }
        if (pausePanel == null || endingCreditsPanel == null || menuPanel == null)
        {
            return; //sai do metodo (funcao)
        }
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

    public void GoToPanel()
    {
        actualPanel.SetActive(false);
        goToPanel.SetActive(true);
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
