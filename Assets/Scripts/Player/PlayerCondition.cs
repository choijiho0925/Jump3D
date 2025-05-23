using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;

    [Header("Condition")]
    public float staminaRecoverDelay; // 회복 딜레이 (초)
    public float lastStaminaUseTime;

    Condition Health { get { return uiCondition.health; } }
    Condition Stamina { get { return uiCondition.stamina; } }

    void Update()
    {
        Health.Subtract(Health.passiveValue * Time.deltaTime);
        AddStamina();

        if(Health.currentValue <= 0f)
        {
            GameManager.Instance.Die();
        }
    }

    private void AddStamina()
    {
        // 스태미나 회복 (소모한 지 일정 시간 지났을 때)
        if (Time.time - lastStaminaUseTime > staminaRecoverDelay)
        {
            Stamina.Add(Stamina.passiveValue * Time.deltaTime);
        }
    }

    public void Heal(float amount)
    {
        Health.Add(amount);
    }

    public void GrowUpStaminaMaxValue(float amount)
    {
        Stamina.maxValue += amount;
    }

    public void JumpForceUp(float amount)
    {
        CharacterManager.Instance.Player.controller.jumpForce += amount;
    }
}
