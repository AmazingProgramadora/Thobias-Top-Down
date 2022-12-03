using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrossfadeTransition : MonoBehaviour
{
    Animator anim;
    public float whenToAppear;

    private void Awake()
    {
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
}
