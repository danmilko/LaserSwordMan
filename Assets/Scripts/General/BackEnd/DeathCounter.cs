using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathCounter : MonoBehaviour
{
    public int totalScore = 0;
    public int levelmult;
    public Count count;
    private void OnEnable()
    {
        totalScore = levelmult * count.count;
        GetComponent<Text>().text = string.Format("Money collected: {0} (count) x {1} (level multiplier) = {2}", count.count, levelmult, totalScore);
    }
}
