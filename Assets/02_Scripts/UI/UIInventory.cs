using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

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
            newSlot.inventory = this;

            slots.Add(newSlot);
        }

        BackButton.onClick.AddListener(CloseInventoryUI);
    }

    private void Update()
    {
        // 아이템이 기존 슬롯 개수보다 많아질 경우 AddSlots 호출
        //if ()
        //{
        //    AddSlots();
        //}
    }

    public void CloseInventoryUI()
    {
        gameObject.SetActive(false);
        UIManager.Instance.UIMainMenu.OpenButtonUI();
    }

    public void AddSlots()
    {
        for (int i = 0; i < 3; i++)
        {
            GameObject slotObj = Instantiate(slotPrefab, slotPanel);
            UISlot newSlot = slotObj.GetComponent<UISlot>();
            newSlot.Index = slotCount;
            newSlot.inventory = this;

            slots.Add(newSlot);
            slotCount++;
        }
    }
}
