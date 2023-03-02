using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.Assertions;

public class LaserCtrl : MonoBehaviour
{
    [SerializeField] float LaserdelayOn; // 켜져있는 시간
    [SerializeField] float LaserdelayOff; // 꺼져있는 시간
    [SerializeField] GameObject LaserGo; // 레이저 객체

    // Start is called before the first frame update
    void Start()
    {
        AutoLaserOnOff();
    }

    // 주어진 조건이 True일 시 에러메세지 호출
    private void AutoLaserOnOff()
    {
        Assert.IsTrue(LaserdelayOn > 0, "LaserdelayOn 값이 입력되지 않았습니다."); // 레이저가 켜져있는 시간이 0보다 작다면 ""출력
        Assert.IsTrue(LaserdelayOff > 0, "LaserdelayOff 값이 입력되지 않았습니다."); // 레이저가 꺼져있는 시간이 0이 아닌것이 아니라면 "'출력
        if (LaserdelayOn == 0 || LaserdelayOff == 0) return;

        StartCoroutine(Co_AutoLaserOnOff());
    }

    IEnumerator Co_AutoLaserOnOff()
    {
        while (true) // 무한 반복
        {
            LaserOn(); // 레이져 켜기
            yield return new WaitForSeconds(LaserdelayOn);
            LaserOff(); // 레이저 끄기
            yield return new WaitForSeconds(LaserdelayOff);
        }
    }

    void LaserOn()
    {
        LaserGo.SetActive(true); // 레이저 켜기
        // 여기에 켜질 때 사운드 등 제어
    }
    void LaserOff()
    {
        // 여기에 꺼질 때 사운드 등 제어
        LaserGo.SetActive(false); // 레이저 끄기
    }
}

