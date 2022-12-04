using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrossfadeTransition : MonoBehaviour
{
    Animator anim;
    public float whenToAppear;
    public GameObject futureTrigger;
    public static CrossfadeTransition instance;
    public GameObject avatarUI, avatarUIPast;

    private void Awake()
    {
        instance = this;
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(DialogueTrigger.EndCrossfadeDialogue == true)
        StartCoroutine(StartTrigger());
    }

    IEnumerator StartTrigger()
    {
        yield return new WaitForSeconds(whenToAppear);
        anim.SetTrigger("EndCrossfade");
    }

    public void EnableTrigger()
    {
        futureTrigger.SetActive(true);
        avatarUI.SetActive(true);
        avatarUIPast.SetActive(false);
    }

}
