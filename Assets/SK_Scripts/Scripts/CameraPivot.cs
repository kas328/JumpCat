//using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    public Transform target;
    // Start is called before the first frame update
    void Start()
    {
        // 1.타겟의 y축으로만 카메라가 따라가고 싶다.
        // - 1. 타겟이 있어야 한다.
        // - 2. 카메라가 있어야 한다. (자기 자신)
        // - 3. y축으로만 추적해야 한다.
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(0, target.position.y, 0);
    }
}
