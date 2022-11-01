using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public string levelToLoad;

    public void RunScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelToLoad);
    }

    public void ExitToDesktop()
    {
        Application.Quit();
        print("The game was closed");
    }

}
