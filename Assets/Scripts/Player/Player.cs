using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condition;
    public Equipment equipment;

    public ItemData itemData;
    public Action addItem;

    public Transform dropPosition;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;
        if (controller == null)
        {
            controller = GetComponent<PlayerController>(); // 방어코드
        }

        if (condition == null)
        {
            condition = GetComponent<PlayerCondition>(); // 방어코드
        }

        if (equipment == null)
        {
            equipment = GetComponent<Equipment>();
        }
    }
}
