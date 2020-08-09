using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanTestMove : MonoBehaviour
{
    enum States { Fall = -1, Idle = 0, Run = 1, Attack = 2, Undefined = 10 };
    enum Attack { Null = 0, Saber, Force, Lights };

    public Jump jumper;
    public Transform forceStart;
    public GameObject force;
    public Camera watcher;
    public WeaponSystem weapon;

    Vector3 tfm;
    Rigidbody2D rb;
    States currentState = States.Idle;
    Attack attackType = Attack.Null;
    bool isFacingRight = true;
    bool attackFinished = true;
    bool grounded = true;
    public int attackStage = 0;
    public GameObject block;

    const float maxSpeed = 5f;

    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        tfm = this.transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        grounded = anim.GetBool("grounded");
        Moving();
        if (!grounded || anim.GetCurrentAnimatorStateInfo(1).IsName("Grounded"))
        {

        }
        else if (Input.GetButtonDown("Saber"))
        {
            Attacking();
        }
        else if (Input.GetButtonDown("Force"))
        {
            AttackForce();
        }
        else if (attackFinished)
        {
            attackStage = -1;
            anim.SetInteger("attackStage", attackStage);
        }
        if (attackFinished)
        {
            anim.SetInteger("state", (int)currentState);
        }
        anim.SetBool("grounded", grounded);
    }

    public void BlockOff()
    {
        block.SetActive(false);
    }
    public void BlockOn()
    {
        block.SetActive(true);
    }

    private void Attacking()
    {
        attackType = Attack.Saber;
        SetAttack();
    }

    private void PlayAttackSound()
    {
        weapon.PlayAttackSound();
    }

    private void AttackForce()
    {
        attackType = Attack.Force;
        SetAttack();
    }

    private void SetAttack()
    {
        if (attackStage == -1)
        {
            switch (attackType)
            {
                case Attack.Null:
                    break;
                case Attack.Saber:
                    anim.Play("Attack1");
                    break;
                case Attack.Force:
                    anim.Play("ForcePush");
                    break;
                case Attack.Lights:
                    break;
                default:
                    break;
            }
        }
        attackStage++;
        currentState = States.Attack;
        attackFinished = false;
        anim.SetInteger("state", (int)currentState);
        anim.SetInteger("attackType", (int)attackType);
        anim.SetInteger("attackStage", attackStage);
        anim.SetBool("attackFinished", attackFinished);
    }

    private void Moving()
    {
        tfm = this.transform.position;
        float move = Input.GetAxis("Horizontal");
        if (anim.GetCurrentAnimatorStateInfo(1).IsName("Grounded"))
        {
            move = 0;
        }
        if (rb.velocity.y < -4.3f && grounded)
        {
            currentState = States.Fall;
            if (grounded == true)
            {
                grounded = false;
            }
            anim.SetBool("grounded", false);
        }
        else if (currentState == States.Fall && !grounded)
        {

        }
        else if (move != 0)
        {
            if (move > 0 && !isFacingRight)
            {
                Flip();
            }
            else if (move < 0 && isFacingRight)
            {
                Flip();
            }
            transform.position = tfm;
            currentState = States.Run;
            
        }
        else
        {
            currentState = States.Idle;
        }
        //if (Math.Abs(rb.velocity.x) < maxSpeed)
        //{
        //    if (rb.velocity.x * move >= 0 && move != 0)
        //    {
        //        rb.AddForce(new Vector2(move * 20, 0));
        //    }
        //    else if (rb.velocity.x > 0.3f)
        //    {
        //        rb.AddForce(new Vector2(-Mathf.Sign(rb.velocity.x) * 20, 0));
        //    }
        //    else
        //    {
        //        rb.velocity = new Vector2(0, rb.velocity.y);
        //    }
        //}
        rb.velocity = new Vector2(move * maxSpeed, rb.velocity.y);
    }

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        //print("Flip");
    }

    private void AttackFinished()
    {
        attackFinished = true;
        anim.SetBool("attackStarted", false);
        anim.SetBool("attackFinished", attackFinished);
    }

    private void AttackStarted()
    {
        anim.SetBool("attackStarted", true);
    }
    private void Grounded()
    {
        grounded = true;
    }

    private void ForcePush()
    {
        GameObject copy = Instantiate(force);
        copy.transform.position = forceStart.position;
        copy.SetActive(true);
        copy.GetComponent<Rigidbody2D>().velocity = new Vector2((Convert.ToSingle(isFacingRight) - 0.5f) * 30, 0);
        AttackFinished();
    }
}
