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

    ItemData selectedItem;
    int selectedItemIndex = -1;
    List<int> curEquipIndexes = new List<int>();

    private void Start()
    {
        for (int i = 0; i < slotCount; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotPanel);
            UISlot newSlot = slotObj.GetComponent<UISlot>();
            newSlot.Index = i;

            slots.Add(newSlot);
        }

        UpdateUI();

        BackButton.onClick.AddListener(CloseInventoryUI);

        GameManager.Instance.Player.addItem += AddItem;
    }

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

    #region 슬롯 관련
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
    #endregion

    #region 아이템 슬롯 선택
    public void SelectItem(int index)
    {
        if (slots[index].item == null) return;

        if (selectedItemIndex == index)
        {
            if (slots[index].Equipped)
            {
                UnEquip(index);
                curEquipIndexes.Remove(index);
                slots[selectedItemIndex].EquipText.gameObject.SetActive(false);
                selectedItemIndex = -1;
                selectedItem = null;
            }
            else
            {
                EquipItem();
            }

            return;
        }

        if (selectedItemIndex != -1)
        {
            slots[selectedItemIndex].EquipText.gameObject.SetActive(false);
        }

        selectedItem = slots[index].item;
        selectedItemIndex = index;
        slots[selectedItemIndex].EquipText.gameObject.SetActive(true);
        UpdateUI();
    }
    #endregion

    #region 아이템 장착 관련
    void EquipItem()
    {
        for (int i = 0; i < curEquipIndexes.Count; i++)
        {
            int equipIndex = curEquipIndexes[i];
            if (slots[equipIndex].item.type == selectedItem.type)
            {
                UnEquip(equipIndex);
                curEquipIndexes.RemoveAt(i);
                break;
            }
        }

        slots[selectedItemIndex].Equipped = true;
        curEquipIndexes.Add(selectedItemIndex);
        UpdateUI();

        // 장비 스텟 적용
        var equipItem = slots[selectedItemIndex].item;
        for (int i = 0; i < equipItem.stats.Length; i++)
        {
            float value = equipItem.stats[i].value;
            switch (equipItem.stats[i].type)
            {
                case ItemStatType.Attack:
                    GameManager.Instance.Player.AddAttack(value);
                    break;
                case ItemStatType.Defense:
                    GameManager.Instance.Player.AddDefense(value);
                    break;
                case ItemStatType.Hp:
                    GameManager.Instance.Player.AddHp(value);
                    break;
                case ItemStatType.Crit:
                    GameManager.Instance.Player.AddCrit(value);
                    break;
            }
        }
    }

    void UnEquip(int index)
    {
        slots[index].Equipped = false;
        UpdateUI();

        // 장비 스텟 해제
        var equipItem = slots[index].item;
        for (int i = 0; i < equipItem.stats.Length; i++)
        {
            switch (equipItem.stats[i].type)
            {
                case ItemStatType.Attack:
                    GameManager.Instance.Player.RemoveAttack(equipItem.stats[i].value);
                    break;
                case ItemStatType.Defense:
                    GameManager.Instance.Player.RemoveDefense(equipItem.stats[i].value);
                    break;
                case ItemStatType.Hp:
                    GameManager.Instance.Player.RemoveHp(equipItem.stats[i].value);
                    break;
                case ItemStatType.Crit:
                    GameManager.Instance.Player.RemoveCrit(equipItem.stats[i].value);
                    break;
            }
        }
    }
    #endregion
}
