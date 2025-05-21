using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition stamina {  get { return uiCondition.stamina; } }

    void Update()
    {
        health.Subtract(health.passiveValue * Time.deltaTime);
        stamina.Add(stamina.passiveValue * Time.deltaTime);

        if(health.currentValue <= 0f)
        {
            GameManager.Instance.Die();
        }
    }
}
