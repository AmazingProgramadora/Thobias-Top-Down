using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowValue : MonoBehaviour
{
    Text textComponent;

    void Start()
    {
        textComponent = GetComponent<Text>();
    }

    public void SetSliderValue(float sliderValue)
    {
        textComponent.text = Mathf.Round(sliderValue + 80).ToString();
        //textComponent.text = Mathf.RoundToInt(sliderValue + 80) + "%";
    }
}