using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Text score;
    public Text BestScore;
    // Start is called before the first frame update
    void Start()
    {
        score.text = (int)(TrapManager.Initiate.score)+"M";
        if(PlayerPrefs.HasKey("best score"))
        {
            BestScore.text= PlayerPrefs.GetInt("best score").ToString() + "M";
        }
        else
        {
            BestScore.text = (int)(TrapManager.Initiate.score) + "M";
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}
