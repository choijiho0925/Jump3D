using System;
using UnityEngine;

public enum ItemType
{
    Equipable,
    Consumable
}

public enum ConsumableType
{
    Health,
    Stamina,
    JumpForce
}

public enum EquipableType
{
    Speed,
    Gravity
}

[Serializable]

public class ItemDataConsumable
{
    public ConsumableType type;
    public float value;
}

[Serializable]

public class ItemDataEquip
{
    public EquipableType type;
    public float value;
}

[CreateAssetMenu(fileName ="Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string displayName;
    public string description;
    public  ItemType type;
    public Sprite icon;
    public GameObject dropPrefab;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackCount;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;

    [Header("Equip")]
    public GameObject equipPrefab;
    public ItemDataEquip[] equipables;
}