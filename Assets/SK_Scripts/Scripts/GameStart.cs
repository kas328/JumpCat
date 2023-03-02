using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameStart : MonoBehaviour
{
    public GameObject button;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    public void OnClick()
    {
        SceneManager.LoadScene("JumpCat");
    }
}

