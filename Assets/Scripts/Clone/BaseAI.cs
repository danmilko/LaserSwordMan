using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAI : MonoBehaviour
{
    public Transform player;
    protected Vector2 direction;
    protected bool isFacingRight = true;
    public bool canGo = true;
    public bool stationary = false;
    protected Vector3 tfm;
    protected Rigidbody2D rb;
    protected States currentState = States.Idle;

    public Animator anim;
    protected bool grounded = true;

    public enum States { Fall = -1, Idle = 0, Run = 1, Attack = 2, Undefined = 10 };

    protected void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
        //print("Flip");
    }

    protected void OnCollisionEnter2D(Collision2D collision)
    {
        grounded = true;
        if (rb != null)
        {
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }
}
