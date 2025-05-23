using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData item;
    public UIInventory inventory;

    public Button button;
    public Image icon;
    public TextMeshProUGUI descriptionText;
    private Outline outline;

    public int index;
    public int quantity;
    public bool equipped;

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    public void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = item.icon;
        descriptionText.text = quantity > 1 ? quantity.ToString() : string.Empty;

        if (outline != null)
        {
            outline.enabled = equipped;
        }
    }

    public void Clear()
    {
        item = null;
        icon.gameObject.SetActive(false);
        descriptionText.text = string.Empty;
    }

    public void OnClickButton()
    {
        inventory.SelectItem(index);
    }

    private void OnEnable()
    {
        outline.enabled = equipped;
    }

}
