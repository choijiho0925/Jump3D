using UnityEngine;

public class UICondition : MonoBehaviour
{
    public Condition health;
    public Condition stamina;

    private void Start()
    {
        if (CharacterManager.Instance.Player.condition.uiCondition == null)
        {
            CharacterManager.Instance.Player.condition.uiCondition = this;
        }
    }
}
