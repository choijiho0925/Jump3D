using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public bool isPlaying;

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
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        isPlaying = true;
        CheckPlayGame();
    }

    public void Die()
    {
        if (isPlaying)
        {
            isPlaying = false; //  게임 정지
            Cursor.lockState = CursorLockMode.None; // 마우스 잠금 풀음
            CharacterManager.Instance.Player.controller.canLook = false; // 화면이동 불가
            UIManager.Instance.gameOverUI.SetActive(true); // 게임오버UI 출력
            CheckPlayGame();
        }
    }

    public void RestartGame()
    {
        SceneManager.sceneLoaded += OnSceneLoaded; // 호출 등록
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

    private void CheckPlayGame()
    {
        if (isPlaying)
        {
            Time.timeScale = 1.0f;
        }
        else
        {
            Time.timeScale = 0.0f;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        isPlaying = true;
        CheckPlayGame();
        SceneManager.sceneLoaded -= OnSceneLoaded; // 중복 호출 방지
    }
}
