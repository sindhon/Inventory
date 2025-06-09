using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public List<UISlot> slots = new List<UISlot>(); // 슬롯 리스트

    [SerializeField] private Button BackButton;
    [SerializeField] private Transform slotPanel;
    [SerializeField] private GameObject slotPrefab;
    private int slotCount = 9;

    private void Start()
    {
        for (int i = 0; i < slotCount; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotPanel);
            UISlot newSlot = slotObj.GetComponent<UISlot>();
            newSlot.Index = i;

            slots.Add(newSlot);
        }

        BackButton.onClick.AddListener(CloseInventoryUI);

        GameManager.Instance.Player.addItem += AddItem;
    }

    //private void Update()
    //{
    //    // 아이템이 기존 슬롯 개수보다 많아질 경우 AddSlots 호출
    //    if ()
    //    {
    //        AddSlots();
    //    }
    //}

    public void CloseInventoryUI()
    {
        gameObject.SetActive(false);
        UIManager.Instance.UIMainMenu.OpenButtonUI();
    }

    public void AddItem()
    {
        ItemData data = GameManager.Instance.Player.itemData;
        if (data == null) return;

        UISlot emptySlot = GetEmptySlot();

        if (emptySlot == null)
        {
            AddSlots();
            emptySlot = GetEmptySlot();
            if (emptySlot == null) return;
        }

        emptySlot.item = data;
        emptySlot.Index = slots.IndexOf(emptySlot);
        UpdateUI();
        GameManager.Instance.Player.itemData = null;
    }

    public void AddSlots()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotPanel);
            UISlot newSlot = slotObj.GetComponent<UISlot>();
            newSlot.Index = slotCount;

            slots.Add(newSlot);
            slotCount++;
        }
    }

    void UpdateUI()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item != null)
            {
                slots[i].SetItem();
            }
            else
            {
                slots[i].Refresh();
            }
        }
    }

    UISlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == null)
            {
                return slots[i];
            }
        }
        return null;
    }
}
