using System;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;
    public UIInventory inventory;

    [Header("Condition")]
    public float staminaRecoverDelay; // ȸ�� ������ (��)
    public float lastStaminaUseTime;

    Condition Health { get { return uiCondition.health; } }
    Condition Stamina { get { return uiCondition.stamina; } }

    [HideInInspector] 
    public bool isEquipHammer;
    public bool isEquipKnife;

    void Update()
    {
        Health.Subtract(Health.passiveValue * Time.deltaTime);
        AddStamina();

        if (Health.currentValue <= 0f)
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

    public void EquipHammer(float amount)
    {
        if (inventory.slots[inventory.selectedItemIndex].equipped == true)
        {
            CharacterManager.Instance.Player.controller.rb.mass -= amount;
        }
        else
        {
            CharacterManager.Instance.Player.controller.rb.mass += amount;
        }
    }

    public void EquipKnife(float amount)
    {
        if (inventory.slots[inventory.selectedItemIndex].equipped == true) 
        {
            CharacterManager.Instance.Player.controller.moveSpeed += amount;
            CharacterManager.Instance.Player.controller.runSpeed += amount;
        }
        else
        {
            CharacterManager.Instance.Player.controller.moveSpeed -= amount;
            CharacterManager.Instance.Player.controller.runSpeed -= amount;
        }
    }
}
