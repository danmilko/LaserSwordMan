using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSaber : WeaponSystem
{
    private void Update()
    {
        if (!source.isPlaying)
        {
            source.PlayOneShot(idle);
        }
    }
}
