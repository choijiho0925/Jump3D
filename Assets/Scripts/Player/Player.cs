using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condition;

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
