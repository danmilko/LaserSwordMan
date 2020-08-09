using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        LiveSystem ls = collision.gameObject.GetComponent<LiveSystem>();
        if (ls != null)
        {
            ls.health = 0;
            ls.Hitted();
            ls.restoreHealth = 0;
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
