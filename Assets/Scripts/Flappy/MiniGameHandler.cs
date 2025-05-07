using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using UnityEngine.UI;
using TMPro;

public enum State
{
    Intro,
    Playing,
    Victory,
    GameOver
}
public class MiniGameHandler : MonoBehaviour
{
    //설명 ui를 먼저 보여줌
    //아무 키를 누르면 게임이 시작됨. 그전엔 모든게 멈춰있음
    //비행기 게임은 30초 제한(inpector창에서 조절할 수 있게)
    //잘 버티면 승리 메시지 출력 후 3초 뒤 메인맵으로 이동
    //실패하면 게임오버 메시지 출력 후 3초 뒤 메인맵으로 이동
    //점수 저장은 다른 스크립트가 담당

    [Serializable]
    public struct UIPanels
    {
        public GameObject introPanel;      // 게임설명 UI
        public GameObject victoryPanel;    // 승리 메시지 UI
        public GameObject gameOverPanel;   // 실패(게임오버) 메시지 UI
    }

    [Header("UI 패널 설정")]
    [SerializeField] UIPanels uiPanels;

    [Header("게임 설정")]
    [SerializeField] float timeLimit = 30.0f;
    [SerializeField] TMP_Text timeText;
    [SerializeField] float resultDelay = 3f;
    [SerializeField] string nextSceneName = "Main";

    [Header("플레이어(비행기) 참조")]
    [SerializeField] PlaneController planeController;

    State currentState;
    float timer;

    private void Awake()
    {
        // 처음에는 인트로 상태
        EnterState(State.Intro);
        Time.timeScale = 0f;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Intro:
                // 아무 키 누르면 게임 시작
                if (Input.anyKeyDown)
                {
                    uiPanels.introPanel.SetActive(false);
                    EnterState(State.Playing);
                }
                break;

            case State.Playing:
                if (planeController != null && planeController.isDead)
                {
                    EnterState(State.GameOver);
                    break;
                }
                // 제한 시간 감소
                timer -= Time.deltaTime;
                if (timeText != null)
                {
                    timeText.text = timer.ToString("F1");
                }

                if (timer <= 0f)
                {
                    EnterState(State.Victory);
                }
                break;
        }
    }
    void EnterState(State state)
    {
        currentState = state;

        switch(state)
        {
            case State.Playing:
                Time.timeScale = 1f;
                timer = timeLimit;
                break;
            case State.Victory:
                Time.timeScale = 0f;
                uiPanels.victoryPanel.SetActive(true);
                StartCoroutine(GoMain());
                break;
            case State.GameOver:
                uiPanels.gameOverPanel.SetActive(true);
                StartCoroutine(GoMain());
                break;

        }
    }
    public void GameOver()
    {
        EnterState(State.GameOver);
    }
    public void Victory()
    {
        EnterState(State.Victory);
    }
    IEnumerator GoMain()
    {
        yield return new WaitForSecondsRealtime(resultDelay);
        Time.timeScale = 1f;
        SceneManager.LoadScene("Main");
    }

}
