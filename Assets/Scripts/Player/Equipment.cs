using UnityEngine;

public class Equipment : MonoBehaviour
{
    public Transform equipTransform;
    public Equip equip;

    private PlayerController controller;
    private PlayerCondition condition;

    void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        condition = CharacterManager.Instance.Player.condition;
    }

    public void EquipNew(ItemData data)
    {
        UnEquip();
        GameObject obj = Instantiate(data.equipPrefab, equipTransform);
        equip = obj.GetComponent<Equip>();
    }

    public void UnEquip()
    {
        if (equip != null)
        {
            Destroy(equip.gameObject);
            equip = null;
        }
    }
}