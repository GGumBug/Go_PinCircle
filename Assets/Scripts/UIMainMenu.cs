using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainMenu : MonoBehaviour
{
    public void ButtonClickEventStart()
    {
        Debug.Log("GameStart");
    }

    public void ButtonClickEventReset()
    {
        Debug.Log("Reset Stage");
    }

    public void ButtonClickEventExit()
    {
        // 현재 실행 환경이 에디터이면 에디터 플레이모드 종료
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //or UnityEditor.EditorApplication.ExitPlaymode();
        //현재 실행 환경이 에디터가 아니면 프로그램 종료
        #else
        Application.Quit();
        #endif
    }
}
