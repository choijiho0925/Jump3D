using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;

    [Header("Condition")]
    public float staminaRecoverDelay; // ȸ�� ������ (��)
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
        // ���¹̳� ȸ�� (�Ҹ��� �� ���� �ð� ������ ��)
        if (Time.time - lastStaminaUseTime > staminaRecoverDelay)
        {
            stamina.Add(stamina.passiveValue * Time.deltaTime);
        }
    }
}
