using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField]
    private StageController stageController;
    [SerializeField]
    private RectTransformMover menuPanel;
    [SerializeField]
    private TextMeshProUGUI textLevelInMenu; 
    [SerializeField]
    private TextMeshProUGUI textLevelInGame; 

    private Vector3 inactivePosition = Vector3.left * 1080;
    private Vector3 activePosition = Vector3.zero;

    private void Awake() {
        // PlayerPrefs.GetInt를 했을때 저장된 값이 없으면 0이 반환 됨
        int index = PlayerPrefs.GetInt("StageLevel");
        textLevelInMenu.text = $"Level {(index+1)}";
    }

    public void ButtonClickEventStart()
    {
        int index = PlayerPrefs.GetInt("StageLevel");
        textLevelInGame.text = $"{(index+1)}";

        menuPanel.MoveTo(AfterStartEvent, inactivePosition);
    }

    private void AfterStartEvent()
    {
        stageController.IsGameStart = true;
    }

    public void ButtonClickEventReset()
    {
        PlayerPrefs.SetInt("StageLevel", 0);
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
        int index = PlayerPrefs.GetInt("StageLevel");
        textLevelInMenu.text = $"Level {(index+1)}";

        menuPanel.MoveTo(AfterStageExitEvent, activePosition);
    }
    private void AfterStageExitEvent()
    {
        int index = PlayerPrefs.GetInt("StageLevel");

        if (index == SceneManager.sceneCountInBuildSettings)
        {
            PlayerPrefs.SetInt("StageLevel", 0);
            SceneManager.LoadScene(0);
            return;
        }

        SceneManager.LoadScene(index);
    }

}
