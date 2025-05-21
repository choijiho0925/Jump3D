using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    private GameObject gameOverUI;

    public void RestartGame()
    {
        Time.timeScale = 1f; // �ٽ� ���� ���
        Cursor.lockState = CursorLockMode.Locked; // ���콺 Ŀ�� ���
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
