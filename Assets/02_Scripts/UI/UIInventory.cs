using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public List<UISlot> slots = new List<UISlot>(); // 인벤토리 슬롯 리스트

    [SerializeField] private Button BackButton;       // 뒤로가기 버튼
    [SerializeField] private Transform slotPanel;     // 슬롯이 생성될 부모 객체
    [SerializeField] private GameObject slotPrefab;   // 슬롯 프리팹
    private int slotCount = 9;                        // 초기 슬롯 개수

    ItemData selectedItem;                            // 현재 선택된 아이템
    int selectedItemIndex = -1;                       // 선택된 아이템의 슬롯 인덱스
    List<int> curEquipIndexes = new List<int>();      // 현재 장착 중인 아이템 인덱스 리스트

    private void Start()
    {
        // 초기 슬롯 생성
        for (int i = 0; i < slotCount; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotPanel);
            UISlot newSlot = slotObj.GetComponent<UISlot>();
            newSlot.Index = i;
            slots.Add(newSlot);
        }

        UpdateUI();

        BackButton.onClick.AddListener(CloseInventoryUI);

        GameManager.Instance.Player.addItem += AddItem;     // 아이템 추가 이벤트 구독
    }

    public void CloseInventoryUI()      // 인벤토리 닫기
    {
        gameObject.SetActive(false);
        UIManager.Instance.UIMainMenu.OpenButtonUI();
    }

    public void AddItem()       // 아이템 추가 처리
    {
        ItemData data = GameManager.Instance.Player.itemData;
        if (data == null) return;

        UISlot emptySlot = GetEmptySlot();

        // 비어있는 슬롯이 없으면 슬롯 추가
        if (emptySlot == null)
        {
            AddSlots();
            emptySlot = GetEmptySlot();
            if (emptySlot == null) return;
        }

        // 아이템 데이터 할당
        emptySlot.item = data;
        emptySlot.Index = slots.IndexOf(emptySlot);

        UpdateUI();
        GameManager.Instance.Player.itemData = null;
    }

    void UpdateUI()     // 슬롯 UI 갱신
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

    public void AddSlots()      // 아이템 슬롯 3개씩 추가
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

    UISlot GetEmptySlot()       // 비어있는 슬롯 찾기
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].item == null)
                return slots[i];
        }
        return null;
    }

    #endregion

    #region 아이템 슬롯 선택
    public void SelectItem(int index)
    {
        if (slots[index].item == null) return;

        // 이미 선택한 슬롯을 다시 클릭한 경우
        if (selectedItemIndex == index)
        {
            if (slots[index].Equipped)
            {
                // 장착 해제
                UnEquip(index);
                curEquipIndexes.Remove(index);
                slots[selectedItemIndex].EquipText.gameObject.SetActive(false);
                selectedItemIndex = -1;
                selectedItem = null;
            }
            else
            {
                // 장착
                EquipItem();
            }
            return;
        }

        // 다른 슬롯을 선택했을 때 이전 텍스트 숨김
        if (selectedItemIndex != -1)
        {
            slots[selectedItemIndex].EquipText.gameObject.SetActive(false);
        }

        // 새로 선택
        selectedItem = slots[index].item;
        selectedItemIndex = index;
        slots[selectedItemIndex].EquipText.gameObject.SetActive(true);
        UpdateUI();
    }

    #endregion

    #region 아이템 장착 관련
    void EquipItem()
    {
        // 동일 타입 장비가 이미 있다면 장착 해제
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

        // 장착 처리
        slots[selectedItemIndex].Equipped = true;
        curEquipIndexes.Add(selectedItemIndex);
        UpdateUI();

        // 스탯 적용
        var equipItem = slots[selectedItemIndex].item;
        foreach (var stat in equipItem.stats)
        {
            float value = stat.value;
            switch (stat.type)
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

        // 스탯 해제
        var equipItem = slots[index].item;
        foreach (var stat in equipItem.stats)
        {
            float value = stat.value;
            switch (stat.type)
            {
                case ItemStatType.Attack:
                    GameManager.Instance.Player.RemoveAttack(value);
                    break;
                case ItemStatType.Defense:
                    GameManager.Instance.Player.RemoveDefense(value);
                    break;
                case ItemStatType.Hp:
                    GameManager.Instance.Player.RemoveHp(value);
                    break;
                case ItemStatType.Crit:
                    GameManager.Instance.Player.RemoveCrit(value);
                    break;
            }
        }
    }

    #endregion
}
