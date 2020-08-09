using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lights : MonoBehaviour
{
    int damage = 200;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<LiveSystem>() != null && !collision.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<LiveSystem>().health -= damage;
            collision.GetComponent<LiveSystem>().Hitted();
        }
    }
}
