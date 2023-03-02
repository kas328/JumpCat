using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    
    float time,velo,ltime;

    public static Vector3 difdir;
    public bool reset;
    public int tryJump, jumpRemain;//점프횟수초기화,점프횟수
    public static int jumpDir;//점프방향
    public float jumpPower,movePower,heightLim;
    public static bool IsMove;
    AudioSource audioSource;
     Vector3 newvec;
    Transform trap;
    MoveState state;
    Animator anim;
    int OneMore=-1;
    enum MoveState
    {
        Idle,
        Jump,
        Canon,
        Sticky,
        IsOnTheWall,
        IsRunOnWall


    }

    // Start is called before the first frame update
    void Start()
    {
        isGravityOn = true;
        IsMove = true;
        trap = TrapManager.Initiate.gameObject.transform;
        audioSource = GetComponent<AudioSource>();
        
        anim = GetComponentInChildren<Animator>();
        if (tryJump == 0)
            tryJump = 2;
        
        if (jumpPower == 0)//미설정일때 초기화
            jumpPower = 2;
        if (movePower == 0)
        {
            movePower = 2;
        }
        if (heightLim == 0)
            heightLim = 2;


        jumpRemain = tryJump;
        //print(difdir);
        state = MoveState.Idle;
        anim.SetBool("OnGround",true);


    }
    public static float gravity;//아래함수 사용후 초기화필요
    private float G;
    //이 함수가 사용되면 중력이 적용된다.

    Vector3 Gravity(float G)
    {
        if (isGravityOn)
        {
        Vector3 grav;
        gravity += G;
        grav = Vector3.down * gravity * Time.deltaTime;
        return grav;
        }
        else
        {
            gravity = 0;
           return Vector3.zero;
        }

    }


    void Update()
        //주인공의 위치가 어느정도 상승하면 아래로 내려간다 높게 오를수록 더 많이내려간다. 기준점과 플레이어의 높이차가 하강량비율이 된다.
    {

        anim.SetBool("IsShoot", Cannon.isCannonShoot);

        MoveProcess();

        ScreenControl();
        
    }

   
    private void ScreenControl()
    {
        float y = transform.position.y;

        if (Cannon.isCannonShoot)
        {
            newvec.y = heightLim;
            difdir = Vector3.up * (Mathf.Pow(y - newvec.y, 2f) + (y - newvec.y) * 2) * Time.deltaTime;//높이차2
            transform.position -= difdir;
        }
        else if (y > heightLim)//리미트보다 상승했을때
        {
            //캐논슛활성할때 플레이어는 서서히 중앙에 위치한다.한계선이 아니라 리미트보다 상승

            newvec.y = heightLim;
            difdir = Vector3.up * (Mathf.Pow(y - newvec.y, 1.2f) + (y - newvec.y)) * Time.deltaTime;//높이차2
            transform.position -= difdir;
        }
    }

    private void MoveProcess()
    {
        if (IsMove)
        {
            // Player 가 이동할때 실행할 코드
            if (Input.GetButtonDown("Fire1"))
            {
                anim.SetTrigger("OnClick");
            }
            else
            {
                anim.ResetTrigger("OnClick");
            }
            if (reset || Input.GetButtonDown("Fire2"))
            {
                this.gameObject.SetActive(true);
                transform.position = Vector3.zero;
                reset = false;
            }
            print("현재상태:" + state);
            print(isGravityOn);
            if (state != MoveState.Jump)
            {
                ltime = 0;
            }
            switch (state)
            {

                case MoveState.Idle:
                    anim.SetBool("OnGround", true);
                    float h = Input.GetAxis("Horizontal");
                    if (h < 0)
                    {
                        anim.SetInteger("Direction", -1);
                    }
                    else if (h > 0)
                    {

                        anim.SetInteger("Direction", 1);
                    }
                    else if (h == 0)
                    {
                        anim.SetInteger("Direction", 0);
                    }
                    transform.position += Vector3.right * h * Time.deltaTime;
                    if (Input.GetButtonDown("Fire1") && jumpRemain > 0)
                    {
                        OneMore = OneMore * -1;
                        gravity = 0;
                        isGravityOn = true;
                        if (jumpDir == 0)
                        {
                            jumpDir = 1;
                        }
                        jumpRemain--;
                        audioSource.Play();
                        state = MoveState.Jump;

                    }
                    transform.position += Gravity(0.1f);
                    break;
                case MoveState.Jump:
                    anim.SetBool("OnGround", false);

                    //if (GetComponent<Rigidbody>().IsSleeping())
                    //{
                    //    GetComponent<Rigidbody>().WakeUp();
                    //    //어이없지만 stay는최적화를 이유로 일정시간이 흐르면 잠든다. 깨우는 구문 강제로 깨우는거 대신 나중에 다른 방법을 찾아보자
                    //}

                    if (Input.GetButtonDown("Fire1") && jumpRemain > 0)
                    {
                        anim.SetTrigger("OneMore");


                        gravity = 0;
                        if (jumpDir == 0)
                        {
                            jumpDir = 1;
                        }
                        jumpRemain--;
                        audioSource.Play();
                        ltime = 0;
                    }
                    isGravityOn = true;
                    if (jumpDir > 0)
                    {
                        anim.SetInteger("Jump", 1);
                    }
                    else
                    {
                        anim.SetInteger("Jump", -1);
                    }
                    ltime += Time.deltaTime;
                    Vector3 dir = (Vector3.up * 2 * jumpPower + Vector3.right * jumpDir * movePower) / 32;
                    transform.position += Vector3.Lerp(dir, Vector3.zero, ltime) + Gravity(0.1f);
                    //if (Vector3.Lerp(dir, Vector3.zero, ltime) == Vector3.zero)
                    //{
                    //    state = MoveState.Idle;
                    //    ltime = 0;
                    //}
                    break;

                case MoveState.Canon:
                    //if (y > heightLim)//
                    //{
                    //    newvec.x = transform.position.x;
                    //    newvec.y = heightLim;
                    //    newvec.z = transform.position.z;
                    //    difdir = transform.position - newvec;//높이차2
                    //    transform.position -= difdir * Time.deltaTime;

                    //}
                    break;
                case MoveState.Sticky:
                    break;
                case MoveState.IsOnTheWall:
                    if (Input.GetButtonDown("Fire1") && jumpRemain > 0)
                    {
                        if (jumpDir == 0)
                        {
                            jumpDir = 1;
                        }
                        //GetComponent<Rigidbody>().velocity = (Vector3.up * 2f + Vector3.right * jumpDir) * jumpPower;
                        jumpRemain--;
                        audioSource.Play();
                        state = MoveState.Jump;
                    }
                    transform.position += Gravity(0.1f);
                    break;

            }




        }
    }


    //특정 오브젝트에만 반응하고 싶다. 혹은 장애물에 온콜리전 엔터를 사용하면 벽과 같은 장애물이고 온트리거라면 함정등의 장애물이여야한다.
    private void OnCollisionEnter2D(Collision2D other)
    {
        jumpRemain = tryJump;
        PlayerMove.jumpDir *= -1;
        anim.ResetTrigger("OneMore");
        
        gravity = 0;
        if (other.gameObject.CompareTag("Wall"))
        {
            state = MoveState.IsOnTheWall;
            anim.SetBool("TouchOnWall",true);

        }
        else if (other.gameObject.CompareTag("Sticky"))
        {
            if (other.gameObject.transform.position.y - 0.28f * other.gameObject.transform.localScale.y < gameObject.transform.position.y)
            {

            }
        }
        if (state == MoveState.IsOnTheWall)
        {
            isGravityOn = false;
        }
        if (other.gameObject.CompareTag("Floor"))
        {
            isGravityOn = false;
            state = MoveState.Idle;
        }
        //if (other.gameObject.CompareTag("Portal"))
        //{
        //    state = MoveState.Jump;
        //}

    }
    public static bool isGravityOn;
    private void OnCollisionStay2D(Collision2D other)
    {
        if (state==MoveState.IsOnTheWall)
        {
            //print("Stay");

            time += Time.deltaTime;
            if (time > 2)
            {
                isGravityOn=true;
            }
            //if (other.gameObject.CompareTag("Portal"))
            //{
            //    state = MoveState.Jump;
            //}


        }
    }
    private void OnCollisionExit2D(Collision2D other)
    {
        isGravityOn = true;
        gravity = 0;
        time = 0;
        if (state == MoveState.IsOnTheWall)
        {
            time = 0;
            state = MoveState.Idle;
           // print("Exit");


        }
        else if (other.gameObject.CompareTag("Sticky"))
        {
        }
        anim.SetBool("TouchOnWall",false);
        //if (other.gameObject.CompareTag("Portal"))
        //{
        //    state = MoveState.Jump;
        //}
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (Cannon.isCannonShoot) return;
    
        if (other.gameObject.CompareTag("Floor"))
        {
            reset=true;
        }


        else if (other.gameObject.CompareTag("DamageTrap"))
        {
            GameOver.Instance.Die();
        }
        else if (other.gameObject.CompareTag("DeadTrap"))
        {
            GameOver.Instance.Die();
        }
        if (other.gameObject.CompareTag("Cannon"))
        {
            print("cannon");

        }

    }
    
    private void OnTriggerStay2D(Collider2D other)
    {



    }
    private void OnTriggerExit2D(Collider2D other)
    {

    }
    


}
