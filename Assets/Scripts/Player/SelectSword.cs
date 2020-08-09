using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectSword : MonoBehaviour
{
    public Sprite[] addSwords;
    Color[] colors =
    {
        new Color(0, 0, 1), // blue
        new Color(0, 0.7f, 0), // green
        new Color(0.7f, 0, 0), // red
        new Color(0.7f, 0.7f, 0) // yellow
    };
    void Start()
    {
        print(Globals.currSword);
        print(colors.Length);
        if (Globals.currSword < 3)
        {
            this.GetComponent<SpriteRenderer>().color = colors[Globals.currSword];
        }
        else
        {
            switch (Globals.currSword)
            {
                case 3:
                    print(addSwords.Length);
                    this.GetComponent<SpriteRenderer>().sprite = addSwords[0];
                    this.GetComponent<SpriteRenderer>().color = colors[2];
                    this.transform.localScale =  new Vector3(1.5f, transform.localScale.y);
                    break;
                case 4:
                    this.GetComponent<SpriteRenderer>().color = colors[Globals.currSword - 1];
                    break;
            }
        }
    }
}
