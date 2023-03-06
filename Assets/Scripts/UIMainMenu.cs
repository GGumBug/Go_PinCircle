using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField]
    private StageController stageController;
    [SerializeField]
    private RectTransformMover menuPanel;
    private Vector3 inactivePosition = Vector3.left * 1080;
    private Vector3 activePosition = Vector3.zero;

    public void ButtonClickEventStart()
    {
        menuPanel.MoveTo(AfterStartEvent, inactivePosition);
    }

    private void AfterStartEvent()
    {
        stageController.IsGameStart = true;
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
    public void StageExit()
    {
        menuPanel.MoveTo(AfterStageExitEvent, activePosition);
    }
    private void AfterStageExitEvent()
    {
        SceneManager.LoadScene(0);
    }
    
}
