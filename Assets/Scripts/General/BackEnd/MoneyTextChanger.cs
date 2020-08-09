using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyTextChanger : MonoBehaviour
{
    private void Start()
    {
        this.GetComponent<Text>().text = "Money: " + Globals.money;
    }
    public void UpdateText()
    {
        this.GetComponent<Text>().text = "Money: " + Globals.money;
    }

    public void OnEnable()
    {
        UpdateText();
    }
}
