using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBar : MonoBehaviour
{
    [SerializeField]
    private Image dataBar;
    [SerializeField]
    private Text dataText;

    public void updateData(float currentValue, float maximumValue)
    {
        dataBar.fillAmount = currentValue / maximumValue;
        dataText.text = currentValue + "/" + maximumValue;
    }

}
