using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShitmanAI : BaseAI
{
    public GameObject explosion;
    public Transform blaster;
    public int attackStage = 0;
    bool canShoot = true;
    public int damage = 200;
    public bool exploding = false;

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
        if (!exploding)
        {
            if (rb != null && !stationary)
            {
                Moving();
                anim.SetInteger("state", (int)currentState);
            }
            //Aim();
            if ((player.position - transform.position).sqrMagnitude < 4f)
            {
                StartCoroutine(Explode());
            }
            //print((player.position - transform.position).sqrMagnitude);
        }
        //if (canShoot && blaster != null)
        //{
        //    Shoot();
        //}
        //currReload = reloadTime;
    }

    private void Moving()
    {
        tfm = this.transform.position;
        if (player != null)
        {
            Vector2 dist = player.position - transform.position;
            float move = Mathf.Sign(dist.x);
            canShoot = false;
            //if (dist.magnitude < 5 || Mathf.Abs(dist.x) < 3)
            //{
            //    if (dist.magnitude >= 2)
            //    {
            //        move = 0;
            //        canShoot = true;
            //    }
            //    else
            //    {
            //        move = -move;
            //        canShoot = false;
            //    }
            //}
            //else
            //{
            //    canShoot = true;
            //}
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

    IEnumerator Explode()
    {
        exploding = true;
        print("EXTERMINATION!");
        anim.Play("Explosion", 0);
        anim.Play("CloneIdle", 1);
        yield return new WaitForSeconds(2);
        if ((player.position - transform.position).sqrMagnitude < 9 && !this.GetComponent<LiveSystem>().dead)
        {
            player.GetComponent<LiveSystem>().health -= damage;
            player.GetComponent<LiveSystem>().Hitted();
        }
        GameObject temp = Instantiate(explosion);
        temp.transform.position = this.gameObject.transform.position;
        temp.SetActive(true);
        Destroy(this.gameObject);
    }
}
