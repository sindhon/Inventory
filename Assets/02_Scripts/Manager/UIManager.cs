using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;  // 싱글톤 인스턴스
    public static UIManager Instance { get { return instance; } }

    [SerializeField] private UIMainMenu uIMainMenu;     // 메인 메뉴 UI 
    [SerializeField] private UIStatus uIStatus;         // 캐릭터 상태창 UI
    [SerializeField] private UIInventory uIInventory;   // 인벤토리 UI

    public UIMainMenu UIMainMenu => uIMainMenu;
    public UIStatus UIStatus => uIStatus;
    public UIInventory UIInventory => uIInventory;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        // UI 초기 상태 설정
        UIMainMenu.gameObject.SetActive(true);
        UIStatus.gameObject.SetActive(false);
        UIInventory.gameObject.SetActive(false);
    }
}
