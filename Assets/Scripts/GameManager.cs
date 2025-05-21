using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isPlaying;

    private static GameManager _instance; // �̱���
    public static GameManager Instance  // ������Ƽ�� ĸ��ȭ
    {
        get
        {
            if (_instance == null)
            {
                _instance = new GameObject("GameManager").AddComponent<GameManager>();
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

        isPlaying = true;
    }

    private void Start()
    {
        if (isPlaying == true)
        {
            Time.timeScale = 1.0f;
        }
        else
        {
            Time.timeScale = 0.0f;
        }

    }

    private void Update()
    {
        Debug.Log(isPlaying);
    }

    public void Die()
    {
        isPlaying = false; //  ���� ����
        Cursor.lockState = CursorLockMode.None; // ���콺 ��� Ǯ��
        CharacterManager.Instance.Player.controller.canLook = false; // ȭ���̵� �Ұ�
        UIManager.Instance.gameOverUI.SetActive(true); // ���ӿ���UI ���
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0); // ���� �������� ��ε�(��Ȱ�̾����� ������ �ʱ�ȭ �����ָ鼭 �� �ڸ����� �����ε� ��Ȱ�� �ƴ϶� �ٽ� �����̶� �ε� �� ���)
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
