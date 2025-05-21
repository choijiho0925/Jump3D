using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject gameUI;
    public GameObject gameOverUI;
    public TextMeshProUGUI promptText;

    private static UIManager _instance; // 싱글톤
    public static UIManager Instance  // 프로퍼티로 캡슐화
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("UIManager").AddComponent<UIManager>();
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (_instance == this)
            {
                Destroy(gameObject);
            }
        }

        gameUI.SetActive(true);

        //if (gameUI == null)                                                방어코드 작성은 해봤지만 주석처리한 이유는 Find의 메모리 비용이 큼
        //{
        //    gameUI = GameObject.Find("GameUI");
        //}

        //if (gameOverUI == null)
        //{
        //    gameOverUI = GameObject.Find("GameOverUI");
        //}

        //if (promptText == null)
        //{
        //    promptText = GameObject.Find("PromptText").GetComponent<TextMeshProUGUI>();
        //}
    }
}
