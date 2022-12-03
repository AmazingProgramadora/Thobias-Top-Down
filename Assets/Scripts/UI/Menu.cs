using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string levelToLoad;
    public Animator transition;

    public void RunScene()
    {
        Time.timeScale = 1f;
        StartCoroutine(TransitionTime());
    }

    public void ExitToDesktop()
    {
        Application.Quit();
        print("The game was closed");
    }

    IEnumerator TransitionTime()
    {
        transition.SetTrigger("StartCrossfade");
        yield return new WaitForSeconds(1.5f);
        DialogueTrigger.EndCrossfadeDialogue = true;
        SceneManager.LoadScene(levelToLoad);
    }
}
