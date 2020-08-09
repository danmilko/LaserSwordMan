using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForcePush : MonoBehaviour
{
    public List<((GameObject, int, Transform), GameObject)> collided = new List<((GameObject, int, Transform), GameObject)>();
    public GameObject temp;
    public float power = 9;
    int count = 0;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>() != null && !collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Sign(GetComponent<Rigidbody2D>().velocity.x) * power, 0));
        }
        if (collision.gameObject.GetComponent<LiveSystem>() != null && collision.gameObject.GetComponent<LiveSystem>().prefab != null && !collision.gameObject.GetComponent<LiveSystem>().addedForce && !collision.gameObject.CompareTag("Player"))
        {
            collided.Add((collision.gameObject.GetComponent<LiveSystem>().Prefab(), collision.gameObject));
            collision.gameObject.GetComponent<LiveSystem>().addedForce = true;
            collision.gameObject.GetComponent<LiveSystem>().SetRigidbodyOn();
        }
    }

    public void DestroyObj()
    {
        for (int i = 0; i < collided.Count; i++)
        {
            if (collided[i].Item2 != null && collided[i].Item2.GetComponent<LiveSystem>().dead || collided[i].Item1.Item1 == null)
            {
                continue;
            }
            Destroy(collided[i].Item2);
            temp = Instantiate(collided[i].Item1.Item1);
            temp.gameObject.GetComponent<LiveSystem>().prefab = collided[i].Item1.Item1;
            temp.name = "Enemy" + count;
            temp.SetActive(true);
            if (temp.GetComponent<CloneAI>() != null)
            {
                temp.GetComponent<CloneAI>().blast.SetActive(false);
            }
            temp.GetComponent<LiveSystem>().health = collided[i].Item1.Item2;
            if (temp != null && collided[i].Item1.Item3 != null)
            {
                temp.transform.position = new Vector2(collided[i].Item1.Item3.position.x, collided[i].Item1.Item3.position.y + 0.5f);
            }
            else
            {
                Destroy(temp);
            }
        }
    }
}
