using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class LiveSystem : MonoBehaviour
{
    public int health;
    public int maxHealth;
    public UnityEvent OnHit;
    public UnityEvent OnDeath;
    private float timeForHealth = 5f;
    public bool dead = false;
    float countDown = 5f;
    public List<Rigidbody2D> parts;
    public List<Collider2D> colls;
    public GameObject prefab;
    public Collider2D platform;
    public bool forceMan;
    //public Collider2D stick;
    public bool addedForce = false;
    float dangerousFall = 0f;
    const float sqrDangerVelocity = 81f;
    public int restoreHealth = 50;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (rb.velocity.sqrMagnitude > sqrDangerVelocity && !forceMan)
        {
            dangerousFall = rb.velocity.sqrMagnitude;
        }
        timeForHealth -= Time.deltaTime;
        if (timeForHealth <= 0)
        {
            timeForHealth = 5;
            health += restoreHealth;
            if (health > maxHealth)
            {
                health = maxHealth;
            }
            OnHit.Invoke();
        }
        if (dead && !this.CompareTag("Player"))
        {
            if (countDown > 0)
            {
                countDown -= Time.deltaTime;
                return;
            }
            Destroy(this.gameObject);
        }
        else if (addedForce && !this.CompareTag("Player"))
        {
            if (countDown > 0)
            {
                countDown -= Time.deltaTime;
                return;
            }
            Respawn();
            countDown = 5f;
        }
    }
    public void Hitted()
    {
        OnHit.Invoke();
        if (health <= 0 && !dead)
        {
            OnDeath.Invoke();
            SetRigidbodyOn();
            if (this.CompareTag("Player"))
            {
                GetComponent<StickmanMoveMobile>().enabled = false;
                GetComponent<JumpMobile>().enabled = false;
            }
            dead = true;
        }
    }

    public void SetRigidbodyOn()
    {
        if (this.GetComponent<Animator>() != null)
        {
            this.GetComponent<Animator>().enabled = false;
            for (int i = 0; i < parts.Count; i++)
            {
                if (parts[i] != null)
                {
                    parts[i].isKinematic = false;
                }
            }
            for (int i = 0; i < colls.Count; i++)
            {
                if (colls[i] != null)
                {
                    colls[i].isTrigger = false;
                }
            }
            if (platform != null)
            {
                platform.enabled = false;
            }
            if (this.GetComponent<CloneAI>() != null)
            {
                this.GetComponent<CloneAI>().enabled = false;
            }
        }
        if (this.gameObject.CompareTag("Player"))
        {

        }
    }

    public void SetRigidbodyOff() //not used
    {
        if (this.GetComponent<Animator>() != null)
        {
            this.GetComponent<Animator>().enabled = true;
            for (int i = 0; i < parts.Count; i++)
            {
                parts[i].isKinematic = true;
            }
            for (int i = 0; i < colls.Count; i++)
            {
                colls[i].isTrigger = true;
            }
            if (this.GetComponent<CloneAI>() != null)
            {
                this.GetComponent<CloneAI>().enabled = true;
            }
        }
    }

    public (GameObject, int, Transform) Prefab()
    {
        return (prefab, health, transform);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (dangerousFall > 0)
        {
            health -= 10 * Mathf.FloorToInt(Mathf.Sqrt(dangerousFall));
            Hitted();
            dangerousFall = 0;
        }
        else if (collision.gameObject.GetComponent<Rigidbody2D>() != null && collision.gameObject.GetComponent<Rigidbody2D>().velocity.sqrMagnitude >= sqrDangerVelocity && !forceMan)
        {
            health -= Mathf.FloorToInt(collision.gameObject.GetComponent<Rigidbody2D>().mass) * Mathf.FloorToInt(collision.gameObject.GetComponent<Rigidbody2D>().velocity.sqrMagnitude);
            Hitted();
        }
    }

    public void Respawn()
    {
        if (dead || prefab == null)
        {
            return;
        }
        GameObject temp = Instantiate(prefab);
        temp.gameObject.GetComponent<LiveSystem>().prefab = prefab;
        temp.name = "Clone";
        temp.SetActive(true);
        if (temp.GetComponent<CloneAI>() != null && temp.GetComponent<CloneAI>().blast != null)
        {
            temp.GetComponent<CloneAI>().blast.SetActive(false);
        }
        temp.GetComponent<LiveSystem>().health = health;
        if (temp != null && transform != null)
        {
            temp.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
        }
        else
        {
            Destroy(temp);
        }
        Destroy(this.gameObject);
    }
}
