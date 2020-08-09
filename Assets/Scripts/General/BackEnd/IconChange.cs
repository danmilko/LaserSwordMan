using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconChange : MonoBehaviour
{
    public List<Sprite> icons;
    public Image curr;
    public int CountLevel = 1;
    public void Inc()
    {
        if (CountLevel < icons.Count - 1)
        {
            CountLevel++;
        }
        UpdateIcon();
    }
    public void Dec()
    {
        if (CountLevel > 1)
        {
            CountLevel--;
        }
        UpdateIcon();
    }
    private void UpdateIcon()
    {
        curr.sprite = icons[CountLevel];
    }
}
