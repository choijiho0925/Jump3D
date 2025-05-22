using UnityEngine;

public enum ItemType
{
    Equipable,
    Consumable
}

public enum ConsumableType
{
    Health,
    Stamina
}

public class ItemDataConsumable
{
    public ConsumableType type;
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
    public GameObject DropPrefab;

    [Header("Stacking")]
    public bool canStack;
    public int maxStackCount;

    [Header("Consumable")]
    public ItemDataConsumable[] consumables;
}