using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelCounter : MonoBehaviour
{
    public int CountLevel = 1;
    const int MaxLevel = 0;
    const int MinLevel = 0;
    public Text text;
    
    public List<string> levelNames;

    public void Inc()
    {
        if (CountLevel < MaxLevel)
        {
            CountLevel++;
        }
        UpdateText();
    }
    public void Dec()
    {
        if (CountLevel < MinLevel)
        {
            CountLevel--;
        }
        UpdateText();
    }
    private void UpdateText()
    {
        text.text = levelNames[CountLevel];
    }
}
