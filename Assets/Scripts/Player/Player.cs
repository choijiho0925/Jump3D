using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;

    private void Start()
    {
        CharacterManager.Instance.Player = this;
        if (controller == null)
        {
            controller = GetComponent<PlayerController>();
        }
    }
}
