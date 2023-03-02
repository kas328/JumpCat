using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Practicemove : MonoBehaviour
{
    public int speed = 5;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        Vector3 dir = h * Vector3.right + v * Vector3.up;
        transform.position += dir * speed * Time.deltaTime;
    }
}

