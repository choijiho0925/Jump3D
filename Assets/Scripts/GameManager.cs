using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool isPlaying;

    private static GameManager _instance; // 싱글톤
    public static GameManager Instance  // 프로퍼티로 캡슐화
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
        isPlaying = false; //  게임 정지
        Cursor.lockState = CursorLockMode.None; // 마우스 잠금 풀음
        CharacterManager.Instance.Player.controller.canLook = false; // 화면이동 불가
        UIManager.Instance.gameOverUI.SetActive(true); // 게임오버UI 출력
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0); // 시작 지점으로 재로딩(부활이었으면 값들을 초기화 시켜주면서 그 자리에서 시작인데 부활이 아니라 다시 시작이라 로드 씬 사용)
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
