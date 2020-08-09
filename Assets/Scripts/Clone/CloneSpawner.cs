using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSpawner : MonoBehaviour
{
    public int MaxClones = 10;
    public GameObject prototype;
    public List<GameObject> temp;
    public float MaxCountDown = 5f;
    float currCountDown = 5f;
    public bool CanSpawn = true;
    public Transform player;

    private void Start()
    {
        temp = new List<GameObject>();
        currCountDown = MaxCountDown;
    }

    void Update()
    {
        currCountDown -= Time.deltaTime;
        if (currCountDown < 0)
        {
            if (temp.Count < MaxClones && CanSpawn && Mathf.Abs(transform.position.y - player.position.y) <= 4f && Mathf.Abs(transform.position.x - player.position.x) >= 3f && Globals.entities < 10)
            {
                temp.Add(Instantiate(prototype));
                int currCount = temp.Count - 1;
                temp[currCount].transform.position = this.gameObject.transform.position;
                temp[currCount].gameObject.GetComponent<LiveSystem>().prefab = prototype;
                temp[currCount].SetActive(true);
                if (temp[currCount].GetComponent<CloneAI>() != null)
                {
                    temp[currCount].GetComponent<CloneAI>().blast.SetActive(false);
                }
                currCountDown = MaxCountDown;
                Globals.entities++;
            }
            else
            {
                //print("trying to remove");
                for (int i = 0; i < temp.Count; i++)
                {
                    if (temp[i] == null)
                    {
                        temp.RemoveAt(i);
                        //print("removed");
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CanSpawn = false;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        CanSpawn = true;
    }
}
