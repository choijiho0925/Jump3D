using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject gameUI;
    public GameObject gameOverUI;
    public TextMeshProUGUI promptText;
    public GameObject restartButton;
    public GameObject exitGameButton;

    private static UIManager _instance; // ½Ì±ÛÅæ
    public static UIManager Instance { get { return _instance; } } //Ä¸½¶È­
    

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (gameUI == null)
        {
            gameUI = GameObject.Find("GameUI");
        }

        if (gameOverUI == null)
        {
            gameOverUI = GameObject.Find("GameOverUI");
        }

        if (promptText == null)
        {
            promptText = GameObject.Find("PromptText").GetComponent<TextMeshProUGUI>();
        }

        if (restartButton == null)
        {
            restartButton = GameObject.Find("RestartButton");
        }

        if (exitGameButton == null)
        {
            exitGameButton = GameObject.Find("ExitGameButton");
        }


        if (restartButton != null)
        {
            Button btn = restartButton.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(OnRestartButtonClicked);
            }
        }

        if (exitGameButton != null)
        {
            Button btn = exitGameButton.GetComponent<Button>();
            if (btn != null)
            {
                btn.onClick.AddListener(OnExitButtonClicked);
            }
        }

        gameOverUI.SetActive(false);
    }

    public void OnRestartButtonClicked()
    {
        if (GameManager.Instance != null)
        {
            GameManager.Instance.RestartGame();
        }
    }

    public void OnExitButtonClicked()
    { 
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ExitGame();
        }
    }

    private void OnDestroy()
    {
        _instance = null;
    }
}
