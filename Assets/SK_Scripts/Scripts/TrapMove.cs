using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapMove : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        PlayerMove.difdir = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= Vector3.up* PlayerMove.difdir.y;
    }

}
