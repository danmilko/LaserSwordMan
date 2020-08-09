using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject followObject;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (followObject != null)
        {
            transform.position = new Vector3(followObject.transform.position.x, followObject.transform.position.y, -0.5f);
        }
    }

    public void RedCamera()
    {

    }
}
