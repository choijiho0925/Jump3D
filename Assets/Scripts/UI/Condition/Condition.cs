using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float currentValue;
    public float startValue;
    public float maxValue;
    public float passiveValue;
    public Image uiBar;

    private void Start()
    {
        currentValue = startValue; // 현재 체력을 시작 체력
    }

    private void Update()
    {
        uiBar.fillAmount = GetPercentage();
    }

    private float GetPercentage()
    {
        return currentValue / maxValue; // 현재체력 / 최대체력으로 체력의 퍼센트 값 구하기
    }

    public void Add(float value)
    {
        currentValue = Mathf.Min(currentValue + value, maxValue); // 현재 체력 + 값, 최대체력중에 작은 값 반환, 최대체력이 넘어가지 않게 하기 위해서
    }

    public void Subtract(float value)
    {
        currentValue = Mathf.Max(currentValue - value, 0); // 현재 체력 - 값, 0중에 큰은 값 반환, 체력이 0보다 아래로 내려가지 못하게 하기 위해서
    }
}
