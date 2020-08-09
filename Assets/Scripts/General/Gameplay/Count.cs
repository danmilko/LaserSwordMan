using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Count : MonoBehaviour
{
    private Text text;
    public int count = 0;
    private void Start()
    {
        text = GetComponent<Text>();
    }

    public void IncreaseCount()
    {
        text.text = string.Format("COUNT: {0}", ++count);
    }

}
