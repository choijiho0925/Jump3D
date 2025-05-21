using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condition;
    public ItemData itemData;
    public Action addItem;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        if (controller == null)
        {
            controller = GetComponent<PlayerController>(); // ����ڵ�
        }

        if (condition == null)
        {
            condition = GetComponent<PlayerCondition>(); // ����ڵ�
        }
    }
}
