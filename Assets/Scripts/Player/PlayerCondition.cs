using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;

    [Header("Condition")]
    public float staminaRecoverDelay; // ȸ�� ������ (��)
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
        // ���¹̳� ȸ�� (�Ҹ��� �� ���� �ð� ������ ��)
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
