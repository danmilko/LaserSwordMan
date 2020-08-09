using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneAI : BaseAI
{
    public Transform blaster;
    public GameObject blast;
    public GameObject startPos;
    public int attackStage = 0;
    float currReload = 1;
    public float reloadTime = 0.5f;
    bool canShoot = true;

    const float maxSpeed = 3f;
    // Start is called before the first frame update
    void Start()
    {
        tfm = this.transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb != null && !stationary)
        {
            Moving();
            anim.SetInteger("state", (int)currentState);
        }
        Aim();
        if (currReload > 0)
        {
            currReload -= Time.deltaTime;
            return;
        }
        if (canShoot && blaster != null)
        {
            Shoot();
        }
        currReload = reloadTime;
    }

    private void Moving()
    {
        tfm = this.transform.position;
        if (player != null)
        {
            Vector2 dist = player.position - transform.position;
            float move = Mathf.Sign(dist.x);
            if (dist.magnitude < 5 || Mathf.Abs(dist.x) < 3)
            {
                if (dist.magnitude >= 2)
                {
                    move = 0;
                    canShoot = true;
                }
                else
                {
                    move = -move;
                    canShoot = false;
                }
            }
            else
            {
                canShoot = true;
            }
            if (rb.velocity.y < -3.5f)
            {
                currentState = States.Fall;
                if (grounded == true)
                {
                    grounded = false;
                }
                canShoot = false;
            }
            else if (currentState == States.Fall && !grounded)
            {
                //currentState = States.Undefined;
            }
            else if (move != 0 && canGo)
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
            if (move != 0 && canGo)
            {
                rb.velocity = new Vector2(Mathf.Sign(move) * maxSpeed, rb.velocity.y);
            }
            else if (!anim.GetCurrentAnimatorStateInfo(1).IsName("CloneIdle"))
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }

    private void Aim()
    {
        if (player != null && blaster != null)
        {
            blaster.LookAt(player, Vector2.up);
            blaster.rotation = Quaternion.Euler(0, 0, blaster.rotation.eulerAngles.x * (isFacingRight ? -1 : 1));
            if (rb != null && rb.velocity.x == 0)
            {
                if (transform.position.x - player.position.x < 0 && !isFacingRight)
                {
                    Flip();
                }
                else if (transform.position.x - player.position.x > 0 && isFacingRight)
                {
                    Flip();
                }
            }
        }
    }

    private void Shoot()
    {
        if (player != null)
        {
            GameObject temp = Instantiate(blast);
            temp.SetActive(true);
            temp.transform.position = startPos.transform.position;
            temp.transform.rotation = blaster.transform.rotation;
            temp.GetComponent<Rigidbody2D>().AddForce((player.position - startPos.transform.position).normalized * 4);
            temp.GetComponent<WeaponSystem>().PlayAttackSound();
        }
    }
}
