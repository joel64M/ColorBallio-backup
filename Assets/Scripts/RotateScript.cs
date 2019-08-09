using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateScript : MonoBehaviour
{
    public float speed = 10f;
    public bool sin = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!sin)
        {
            transform.Rotate(Vector3.up * speed * Time.deltaTime);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, Mathf.Sin(Time.time)* speed*2, 0);
        }
    }
}
