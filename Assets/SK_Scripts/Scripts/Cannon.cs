using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    // 캐논에 닿으면 캐릭터가 들어와야 한다.
    // 캐릭터가 들어오면 위로 발사되어야 한다.
    public float cannonSpeed = 5.0f;
    float cannonTimer;
    float invincibleTimer;
    public float cannonDuration = 2;
    public float invincibleDuration = 2;
    // 캐논이 쏴주고 있는지 여부
    public static bool isCannonShoot = false;
    bool isInvincible = false;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
    }


    void Update()
    {

        // 1. 캐논하고 부딪혔으니까
        // 만약 캐논과 부딪혔다면
        if (isCannonShoot == true)
        {
            // 2. 플레이어가 일정 시간동안 이동 중이니까
            // - 시간이 흐르고
            cannonTimer += Time.deltaTime;

            // - 일정시간 이내라면
            if (cannonTimer < cannonDuration)
            {
                // - 이동
                player.GetComponent<PlayerMove>().jumpRemain = 3;
                player.transform.position += Vector3.up * cannonSpeed * Time.deltaTime;
                PlayerMove.IsMove = false;
            }
            // 3. 그렇지 않고 플레이어 이동이 끝났으니까
            else
            {
                // 4. 2초간 무적유지
                // 무적
                isInvincible = true;
                cannonTimer = 0;
                isCannonShoot = false;
                PlayerMove.IsMove = true;
                PlayerMove.gravity = 0;

            }
        }

        // 무적상태라면
        if (isInvincible)
        {
            InvincibleTime();
        }
        else
        {
            invincibleTimer = 0;
        }

    }
    // 캐논을 발사하기 시작한 순간부터 캐릭터가 움직인 위치 = 플레이어의 현재 위치 - 캐논의 위치
    // 1. 만약 캐논이 실행되면
    // 2. 플레이어의 현재 위치 - 캐논의 위치의 거리를 계산
    // 3. 거리가 50이 넘어가면
    // 4. 캐논 실행 종료
    // transform.eulerAngles = new Vector3(0,0,45); //회전
    public void InvincibleTime()
    {
        invincibleTimer += Time.deltaTime;
        if(invincibleTimer > invincibleDuration)
        {
            isInvincible = false;
        }
    }



    private void OnTriggerEnter2D(Collider2D other)
    {
        // 만약 부딪힌 녀석이 플레이어라면
        if (other.gameObject.name.Contains("Player"))
        {
            isCannonShoot = true;
            PlayerMove.gravity = 0;

            print("cannon");
        }
    }
}

//* 만약 캐논과 플레이어가 닿으면 위로 발사되어야 한다.
// 1. Player 에 있다. -> 부딪힌 녀석
//GameObject player = other.gameObject;
//PlayerMove pm = player.GetComponent<PlayerMove>();
// 2. 캐논이 발사중이라는 것을 알려야 한다.
//isCannonShoot = true;
