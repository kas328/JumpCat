using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    Material mr;
    // Start is called before the first frame update
    void Start()
    {
        mr = GetComponent<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        mr.mainTextureOffset += Vector2.up * PlayerMove.difdir/transform.localScale;
    }
}
