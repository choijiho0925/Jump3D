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

    private static UIManager _instance; // �̱���
    public static UIManager Instance  // ������Ƽ�� ĸ��ȭ
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

        //if (gameUI == null)                                                ����ڵ� �ۼ��� �غ����� �ּ�ó���� ������ Find�� �޸� ����� ŭ
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
