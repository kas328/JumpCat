using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapManager : MonoBehaviour
{
    public static TrapManager Initiate;
    public float score, trapTimer,decTimer;
    float timer;
    public GameObject[] trapFactory;
    Vector3 trapPos;
    // Start is called before the first frame update
    void Start()
    {
        if (Initiate == null)
        {
            Initiate = this;
        }
        if (trapTimer == 0)
        {
            trapTimer = 20;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //플레이어가 일정이상 올라가면 트랩을 생성하고 싶다.
        //필요속성 : 플레이어가 리미트이상올라서 아래로 눌려지는 양만큼이 누적되면 점수가 된다. 이 숫자가 일정이상 누적되면 트랩을 무작위로 생성한다.
        score += PlayerMove.difdir.y;
        timer += PlayerMove.difdir.y;
        if (timer > decTimer)
        {
            GameObject trap= Instantiate(trapFactory[Random.Range(0, trapFactory.Length)]);
            if (trap.name.Contains("Portal"))
            {
                trapPos.x = 0;
            }
            else if (trap.name.Contains("SpearTrap"))
            {
                trapPos.x = 4.13f;
            }
            else if (trap.name.Contains("BladeTrap"))
            {
                trapPos.x = -4.1f;
            }
            else if (trap.name.Contains("Laser"))
            {
                trap.transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 180));
            }
            else if (trap.name.Contains("SawTrap"))
            {
                trap.transform.rotation = Quaternion.Euler(90 * Random.Range(0, 2), 90, 0);
            }
            else if (trap.name.Contains("LightningLine"))
            {
                trapPos.x = 0;
            }
            else
            {
                trapPos.x = Random.Range(-2.5f, 2.5f);

            }
            trapPos.y = transform.position.y;
            trap.transform.position = trapPos;

            timer = 0;
            decTimer=trapTimer-score * 0.01f;
        }
    }
}
