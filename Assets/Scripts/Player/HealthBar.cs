using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    public LiveSystem player;
    private Image bar;

    private void Start()
    {
        player.OnHit.AddListener(ChangeBar);
        bar = this.GetComponent<Image>();
    }

    private void ChangeBar()
    {
        bar.fillAmount = (float)player.health / (float)player.maxHealth;
        float r = 1 - (float)player.health / (float)player.maxHealth;
        float g = (float)player.health / (float)player.maxHealth;
        bar.color = new Color(r, g, 0);
    }
}
