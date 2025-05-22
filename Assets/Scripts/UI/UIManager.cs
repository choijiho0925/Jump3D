using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject gameUI;
    public GameObject gameOverUI;
    public TextMeshProUGUI promptText;

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

        gameOverUI.SetActive(false);
    }

    private void OnDestroy()
    {
        _instance = null;
    }
}
