using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CanGo : MonoBehaviour
{
    public BaseAI clone;
    private void OnTriggerExit2D(Collider2D collision)
    {
        clone.canGo = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        clone.canGo = true;
    }
}
