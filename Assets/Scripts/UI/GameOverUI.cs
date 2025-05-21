using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    private GameObject gameOverUI;

    public void RestartGame()
    {
        Time.timeScale = 1f; // 다시 게임 재생
        Cursor.lockState = CursorLockMode.Locked; // 마우스 커서 잠금
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
