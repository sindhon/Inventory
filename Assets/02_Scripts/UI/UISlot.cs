using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class UISlot : MonoBehaviour
{
    public UIInventory inventory;

    [SerializeField] private Button button;   // 인벤토리 슬롯 버튼
    [SerializeField] private Image icon;      // 아이템 아이콘
    [SerializeField] private int index;       // 인벤토리 슬롯 인덱스
    [SerializeField] private bool equipped;   // 아이템이 장착되었는지 확인
    private Outline outline;

    public int Index { get { return index; } set { index = value; } }

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }

    private void OnEnable()
    {
        outline.enabled = equipped;
    }

    void SetItem()
    {
        icon.gameObject.SetActive(true);    // 아이콘 활성화
    }

    void Refresh()
    {
        icon.gameObject.SetActive(false);   // 아이콘 비활성화
    }
}
