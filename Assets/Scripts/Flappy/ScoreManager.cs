using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }
    public int Score { get; private set; }
    public int BestScore { get; private set; }

    [Header("FlappyPlane �� UI")]
    [SerializeField] TMP_Text CurrentScoreText;
    [Header("Main �� UI")]
    [SerializeField] TMP_Text MainCurrentScoreText;
    [SerializeField] TMP_Text BestScoreText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            BestScore = PlayerPrefs.GetInt("BestScoreText", 0);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    private void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;

    private void Start()
    {
        Score = 0;
        UpdateCurrentScoreUI();
        UpdateBestScoreUI();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "FlappyPlane")
        {
            Score = 0;
            // �÷��� �������� CurrentScoreText��
            CurrentScoreText = GameObject.Find("CurrentScoreText")?.GetComponent<TMP_Text>();
            UpdateCurrentScoreUI();
        }
        else if (scene.name == "Main")
        {
            // ���� �������� �� �� ���
            MainCurrentScoreText = GameObject.Find("MainScoreText")?.GetComponent<TMP_Text>();
            BestScoreText = GameObject.Find("BestScoreText")?.GetComponent<TMP_Text>();

            // ���� ���� �� UI ����
            SaveBestScoreIfNeeded();
            UpdateCurrentScoreUI();
        }
    }

    //����� ���ӿ��� ���� ���� �ø���
    public void AddScore(int amount)
    {
        Score += amount;
        UpdateCurrentScoreUI();
    }

    public void SaveBestScoreIfNeeded()
    {
        if (Score > BestScore)
        {
            BestScore = Score;
            PlayerPrefs.SetInt("BestScore", BestScore);
            PlayerPrefs.Save();
        }
        UpdateBestScoreUI();
    }

    private void UpdateCurrentScoreUI()
    {
        // �÷��� ��
        if (CurrentScoreText != null)
            CurrentScoreText.text = Score.ToString();
        // ���� ��
        if (MainCurrentScoreText != null)
            MainCurrentScoreText.text = Score.ToString();
    }

    private void UpdateBestScoreUI()
    {
        if (BestScoreText != null)
            BestScoreText.text = BestScore.ToString();
    }
}
