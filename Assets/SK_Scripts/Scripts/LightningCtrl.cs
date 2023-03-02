using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;
using UnityEngine.Assertions;

public class LightningCtrl : MonoBehaviour
{
    [SerializeField] float LightningdelayOn; // 켜져있는 시간
    [SerializeField] float LightningdelayOff; // 꺼져있는 시간
    [SerializeField] GameObject LightningGo;

    // Start is called before the first frame update
    void Start()
    {
        AutoLightningOnOff();
    }

    // 주어진 조건이 True일 시 에러메세지 호출
    private void AutoLightningOnOff()
    {
        StartCoroutine(Co_AutoLightningOnOff());
    }

    IEnumerator Co_AutoLightningOnOff()
    {
        while (true) 
        {
            LightningOn(); 
            yield return new WaitForSeconds(LightningdelayOn);
            LightningOff(); 
            yield return new WaitForSeconds(LightningdelayOff);
        }
    }

    void LightningOn()
    {
        LightningGo.SetActive(true); 
        // 여기에 켜질 때 사운드 등 제어
    }
    void LightningOff()
    {
        // 여기에 꺼질 때 사운드 등 제어
        LightningGo.SetActive(false); 
    }
}
