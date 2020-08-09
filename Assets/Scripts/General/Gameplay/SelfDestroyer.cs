using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestroyer : MonoBehaviour
{
    public float time = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        if (time <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
