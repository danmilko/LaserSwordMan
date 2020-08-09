using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayLevel : MonoBehaviour
{
    private static int count = 0;
    public static void LoadLevel(int level)
    {
        SceneManager.LoadScene(level);
    }
    public void LoadLevel1(int level)
    {
        SceneManager.LoadScene(level);
    }
    public static void LoadLevel(Text t)
    {
        SceneManager.LoadScene(t.text);
    }
    public static void LoadLevel(IconChange t)
    {
        SceneManager.LoadScene(t.CountLevel);
    }
    public static void LoadLevel(ScrollElems e)
    {
        SceneManager.LoadScene(e.indexMinDist + 2);
    }
    public static void IncCount()
    {
        count++;
    }
    public static void DecCount()
    {
        count--;
    }
}
