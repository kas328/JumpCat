using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    AudioSource au;
    // Start is called before the first frame update
    void Start()
    {
        au = GetComponent<AudioSource>();       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnSound()
    {
        au.Play();
    }
}
