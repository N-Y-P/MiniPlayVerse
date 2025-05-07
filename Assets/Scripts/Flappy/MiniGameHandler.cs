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
    //���� ui�� ���� ������
    //�ƹ� Ű�� ������ ������ ���۵�. ������ ���� ��������
    //����� ������ 30�� ����(inpectorâ���� ������ �� �ְ�)
    //�� ��Ƽ�� �¸� �޽��� ��� �� 3�� �� ���θ����� �̵�
    //�����ϸ� ���ӿ��� �޽��� ��� �� 3�� �� ���θ����� �̵�
    //���� ������ �ٸ� ��ũ��Ʈ�� ���

    [Serializable]
    public struct UIPanels
    {
        public GameObject introPanel;      // ���Ӽ��� UI
        public GameObject victoryPanel;    // �¸� �޽��� UI
        public GameObject gameOverPanel;   // ����(���ӿ���) �޽��� UI
    }

    [Header("UI �г� ����")]
    [SerializeField] UIPanels uiPanels;

    [Header("���� ����")]
    [SerializeField] float timeLimit = 30.0f;
    [SerializeField] TMP_Text timeText;
    [SerializeField] float resultDelay = 3f;
    [SerializeField] string nextSceneName = "Main";

    [Header("�÷��̾�(�����) ����")]
    [SerializeField] PlaneController planeController;

    State currentState;
    float timer;

    private void Awake()
    {
        // ó������ ��Ʈ�� ����
        EnterState(State.Intro);
        Time.timeScale = 0f;
    }

    private void Update()
    {
        switch (currentState)
        {
            case State.Intro:
                // �ƹ� Ű ������ ���� ����
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
                // ���� �ð� ����
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
