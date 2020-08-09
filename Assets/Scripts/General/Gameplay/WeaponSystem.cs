using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSystem : MonoBehaviour
{
    public int damage;
    public bool isAlly;
    protected Animator anim;
    public AudioClip idle;
    public AudioClip attack;
    public AudioClip hit;
    public AudioSource source;
    void Start()
    {
        if (isAlly)
        {
            anim = transform.parent.parent.GetComponent<Animator>();
            if (source == null)
            {
                source = GetComponent<AudioSource>();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isAlly && collision.transform.root.CompareTag("Enemy") && anim.GetInteger("state") == 2 && anim.GetInteger("attackType") == 1 && collision.transform.root.GetComponent<LiveSystem>() != null)
        {
            collision.transform.root.GetComponent<LiveSystem>().health -= damage;
            collision.transform.root.GetComponent<LiveSystem>().Hitted();
            source.PlayOneShot(hit);
        }
        else if (!isAlly && collision.gameObject.CompareTag("Player"))
        {
            collision.transform.root.GetChild(0).GetComponent<LiveSystem>().health -= damage;
            collision.transform.root.GetChild(0).GetComponent<LiveSystem>().Hitted();
            //source.PlayOneShot(hit);
        }
        else if (isAlly && collision.transform.CompareTag("Destroyable") && anim.GetInteger("state") == 2 && anim.GetInteger("attackType") == 1)
        {
            Destroy(collision.gameObject);
        }
        if (this.CompareTag("Blast") && !collision.transform.root.CompareTag("Enemy"))
        {
            Destroy(this.gameObject);
        }
    }

    public void PlayAttackSound()
    {
        source.PlayOneShot(attack);
    }
}
