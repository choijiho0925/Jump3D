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
        currentValue = startValue; // ���� ü���� ���� ü��
    }

    private void Update()
    {
        uiBar.fillAmount = GetPercentage();
    }

    private float GetPercentage()
    {
        return currentValue / maxValue; // ����ü�� / �ִ�ü������ ü���� �ۼ�Ʈ �� ���ϱ�
    }

    public void Add(float value)
    {
        currentValue = Mathf.Min(currentValue + value, maxValue); // ���� ü�� + ��, �ִ�ü���߿� ���� �� ��ȯ, �ִ�ü���� �Ѿ�� �ʰ� �ϱ� ���ؼ�
    }

    public void Subtract(float value)
    {
        currentValue = Mathf.Max(currentValue - value, 0); // ���� ü�� - ��, 0�߿� ū�� �� ��ȯ, ü���� 0���� �Ʒ��� �������� ���ϰ� �ϱ� ���ؼ�
    }
}
