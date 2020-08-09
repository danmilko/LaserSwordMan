using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    Rigidbody2D parent;
    Animator anim;
    Vector2 jump = new Vector2(0, 20);
    public int jumpState = 0;
    public int maxJump = 30;
    bool grounded;
    public bool ForceMan = true;
    // Start is called before the first frame update
    void Start()
    {
        parent = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        grounded = anim.GetBool("grounded");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        grounded = anim.GetBool("grounded");
        if (Input.GetAxis("Vertical") > 0 && jumpState < maxJump)
        {
            if (ForceMan)
            {
                if (jumpState == 0)
                {
                    anim.Play("Jump", 1);
                }
                jumpState++;
                parent.AddForce(jump * parent.mass);
            }
            else
            {
                jumpState++;
                parent.velocity = new Vector2(parent.velocity.x, maxJump);
            }
        }
        else if (Input.GetAxis("Vertical") < -0.5f && !grounded && Math.Abs(parent.velocity.y) < 20 && ForceMan)
        {
            if (Math.Abs(parent.velocity.y) < 10)
            {
                parent.AddForce(jump * parent.mass * -1);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        jumpState = 0;
        grounded = true;
        anim.SetBool("grounded", true);
        if (anim.GetInteger("state") == -1)
        {
            anim.Play("Grounded", 1);
            anim.Play("Grounded", 0);
        }
        parent.velocity = new Vector2(0, parent.velocity.y);
    }
}
