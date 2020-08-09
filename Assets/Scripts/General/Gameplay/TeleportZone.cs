using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("Triggered");
        Transform tmp;
        if (collision.gameObject.CompareTag("Player"))
        {
            tmp = collision.gameObject.transform.root.GetChild(0);
        }
        else
        {
            tmp = collision.gameObject.transform.root;
        }
        if (tmp.position.y < -20)
        {
            //print("y");
            tmp.position = new Vector3(tmp.position.x, 20, tmp.position.z);
            tmp.GetComponent<Rigidbody2D>().velocity = new Vector2(tmp.GetComponent<Rigidbody2D>().velocity.x, -4.5f);
        }
        if (tmp.position.x > 30)
        {
            //print("x30");
            tmp.position = new Vector3(-29, tmp.position.y, tmp.position.z);
        }
        if (tmp.position.x < -30)
        {
            //print("x-30");
            tmp.position = new Vector3(29, tmp.position.y, tmp.position.z);
        }
    }
}
