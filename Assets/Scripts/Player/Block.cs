using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    public Animator anim;
    AnimatorStateInfo curr;
    System.Random rnd;
    public AudioSource src;
    public AudioClip hit;

    private void Start()
    {
        rnd = new System.Random();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        curr = anim.GetCurrentAnimatorStateInfo(0);
        if (collision.gameObject.CompareTag("Blast") && !curr.IsTag("NoBlock"))
        {
            anim.Play(string.Format("Block{0}", rnd.Next(1, 3)));
            src.PlayOneShot(hit);
        }
    }
}
