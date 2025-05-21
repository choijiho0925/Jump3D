using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;
    public GameObject gameOverUI;

    Condition health { get { return uiCondition.health; } }
    Condition stamina {  get { return uiCondition.stamina; } }

    private void Start()
    {
        if (gameOverUI == null)
        {
            
        }
    }

    void Update()
    {
        health.Subtract(health.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if(health.currentValue <= 0f)
        {
            Die();
        }
    }

    private void Die()
    {
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        gameOverUI.SetActive(true);
    }
}
