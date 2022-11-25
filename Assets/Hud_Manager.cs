using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud_Manager : MonoBehaviour
{
    public int numberKeys;
    public GameObject keyHud1,keyHud2,keyHud3;

    public static Hud_Manager instance;

    private void Start()
    {
        instance = this;
    }
    public void CheckKeys()
    {
        numberKeys = 0;
        foreach(GameObject key in ManagerPlayer.Instance.keyList)
        {
            numberKeys++;
            print(numberKeys);
        }
        if (ManagerPlayer.Instance.keyList.Count < numberKeys)
            numberKeys--;

        ShowKeys();
    }
    private void ShowKeys()
    {
        //print(numberKeys);
        switch (numberKeys) {
            case 0:
                keyHud1.SetActive(false);
                keyHud2.SetActive(false);
                keyHud3.SetActive(false);
                break;


            case 1:                
                keyHud1.SetActive(true);
                keyHud2.SetActive(false);
                keyHud3.SetActive(false);
                break;

            case 2:
                keyHud1.SetActive(true);
                keyHud2.SetActive(true);
                keyHud3.SetActive(false);

                break;

            case 3:
                keyHud1.SetActive(true);
                keyHud2.SetActive(true);
                keyHud3.SetActive(true);

                break;
        }




    }

}
