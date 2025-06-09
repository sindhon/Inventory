using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UISlot : MonoBehaviour
{
    [SerializeField] private Button button;     // 인벤토리 슬롯 버튼
    [SerializeField] private Image icon;        // 아이템 아이콘
    [SerializeField] private TextMeshProUGUI equipText; // 장착 및 해제 텍스트
    [SerializeField] private int index;         // 인벤토리 슬롯 인덱스
    [SerializeField] private bool equipped;     // 아이템이 장착되었는지 확인
    private Outline outline;

    public ItemData item;

    public TextMeshProUGUI EquipText => equipText;
    public int Index { get { return index; } set { index = value; } }
    public bool Equipped { get { return equipped; } set { equipped = value; } }

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void OnEnable()
    {
        outline.enabled = equipped;
    }

    public void SetItem()
    {
        icon.gameObject.SetActive(true);    // 아이콘 활성화
        icon.sprite = item.icon;

        if (outline != null)
        {
            outline.enabled = equipped;
        }

        if (equipped)
            equipText.text = "unequip";
        else
            equipText.text = "equip";
    }

    public void Refresh()
    {
        item = null;
        icon.gameObject.SetActive(false);       // 아이콘 비활성화
        equipText.gameObject.SetActive(false);  // 장착 텍스트 비활성화
    }

    public void OnClickButton()
    {
        UIManager.Instance.UIInventory.SelectItem(index);
    }
}
