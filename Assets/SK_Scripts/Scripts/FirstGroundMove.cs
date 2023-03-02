using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstGroundMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(1).position -= PlayerMove.difdir * Time.deltaTime * 6;
        transform.GetChild(2).position -= PlayerMove.difdir * Time.deltaTime * 5;
        transform.GetChild(3).position -= PlayerMove.difdir * Time.deltaTime * 4;
        transform.GetChild(4).position -= PlayerMove.difdir * Time.deltaTime * 3;
        transform.GetChild(5).position -= PlayerMove.difdir * Time.deltaTime * 2;
        transform.GetChild(6).position -= PlayerMove.difdir * Time.deltaTime * 1;

    }
}
