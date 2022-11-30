using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hud_Manager : MonoBehaviour
{
    public int numberKeys;
    public Image keyHud1,keyHud2,keyHud3;


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
            //print(numberKeys);
        }
        if (ManagerPlayer.Instance.keyList.Count < numberKeys)
            numberKeys--;

        ShowKeys();
    }
    public void GetCorrectSprite(Sprite keySprite)
    {
        switch (numberKeys) {

            case 0:
                keyHud1.sprite = keySprite;
                break;
            case 1:
                keyHud2.sprite = keySprite;
                break;
            case 2:
                keyHud3.sprite = keySprite;
                break;
        }

    }

    private void ShowKeys()
    {
        //print(numberKeys);
        switch (numberKeys) {
            case 0:               
                keyHud1.enabled = false;
                keyHud2.enabled = false;
                keyHud3.enabled = false;
                break;


            case 1:
                //keyHud1.sprite = keySprite.sprite;
                keyHud1.enabled = true;
                keyHud2.enabled = false;
                keyHud3.enabled = false;
                break;

            case 2:
                keyHud1.enabled = true;
                keyHud2.enabled = true;
                keyHud3.enabled = false;

                break;

            case 3:
                keyHud1.enabled = true;
                keyHud2.enabled = true;
                keyHud3.enabled = true;

                break;
        }




    }

}
