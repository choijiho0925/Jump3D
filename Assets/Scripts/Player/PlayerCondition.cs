using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;

    [Header("Condition")]
    public float staminaRecoverDelay; // 회복 딜레이 (초)
    public float lastStaminaUseTime;

    Condition health { get { return uiCondition.health; } }
    Condition stamina { get { return uiCondition.stamina; } }

    void Update()
    {
        health.Subtract(health.passiveValue * Time.deltaTime);
        AddStamina();

        if(health.currentValue <= 0f)
        {
            GameManager.Instance.Die();
        }
    }

    private void AddStamina()
    {
        // 스태미나 회복 (소모한 지 일정 시간 지났을 때)
        if (Time.time - lastStaminaUseTime > staminaRecoverDelay)
        {
            stamina.Add(stamina.passiveValue * Time.deltaTime);
        }
    }
}
