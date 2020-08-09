using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    public LiveSystem prototype;
    public WeaponSystem weapon;

    public int difficulty = 1;
    public const int addingDamage = 5;
    public const int addingHealth = 25;

    public const float IncreaseTime = 10f;
    float countdown = IncreaseTime;

    private void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0)
        {
            countdown = IncreaseTime;
            difficulty++;
            weapon.damage += addingDamage;
            prototype.health += addingHealth;
            prototype.maxHealth += addingHealth;
        }
    }

}
