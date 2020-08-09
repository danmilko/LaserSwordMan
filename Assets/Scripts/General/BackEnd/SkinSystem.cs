using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinSystem : MonoBehaviour
{
    public Sprite[] skins;
    const int p_c = 6;
    public SpriteRenderer[] parts;

    private void Start()
    {
        SelectSkin(Globals.currSkin);
    }

    public void SelectSkin(int skin)
    {
        parts[0].sprite = skins[p_c * skin];          //head
        parts[1].sprite = skins[p_c * skin + 1];      //body
        parts[2].sprite = skins[p_c * skin + 2];      //left arm (high)
        parts[3].sprite = skins[p_c * skin + 3];      //(low)
        parts[4].sprite = skins[p_c * skin + 2];      //right arm
        parts[5].sprite = skins[p_c * skin + 3];
        parts[6].sprite = skins[p_c * skin + 4];      //left leg
        parts[7].sprite = skins[p_c * skin + 5];
        parts[8].sprite = skins[p_c * skin + 4];      //right leg
        parts[9].sprite = skins[p_c * skin + 5];
    }
}
