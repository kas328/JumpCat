using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public static GameOver Instance;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Die()
    { int currentscore = (int)(TrapManager.Initiate.score);
        if (PlayerPrefs.GetInt("best score", currentscore) < currentscore)
        {
            PlayerPrefs.SetInt("best score", currentscore);
        }
        PlayerMove.gravity = 0;
        PlayerMove.difdir = Vector3.zero;
        //GameObject.Find("Player").transform.position = Vector3.zero;
        gameObject.SetActive(false);
        SceneManager.LoadScene("GameOverScene");
    }
}
