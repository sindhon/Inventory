using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject ButtonUI;       // 상태창 및 인벤토리 버튼들을 포함한 UI 오브젝트

    [SerializeField] private Button StatusButton;       // 상태창 버튼
    [SerializeField] private Button InventoryButton;    // 인벤토리 버튼
    [SerializeField] private TextMeshProUGUI nameText;  // 플레이어 이름
    [SerializeField] private TextMeshProUGUI levelText; // 플레이어 레벨
    [SerializeField] private TextMeshProUGUI expText;   // 플레이어 경험치
    [SerializeField] private Image uiBar;               // 플레이어 경험치바
    [SerializeField] private ItemData[] items;          // 아이템 목록 (아이템 추가용)

    private void Start()
    {
        SetPlayerInfo();    // 플레이어 정보(이름, 레벨) UI 갱신

        StatusButton.onClick.AddListener(OpenStatus);
        InventoryButton.onClick.AddListener(OpenInventory);

        GameManager.Instance.Player.OnInfoChanged += SetPlayerInfo; // 플레이어 정보 변경 시 UI 갱신 이벤트 구독
    }

    #region UI 전환
    public void OpenMainMenu()      // 메인 메뉴 열기
    {
        gameObject.SetActive(true);
        OpenButtonUI();
    }

    public void OpenStatus()        // 상태창 UI 열기
    {
        UIManager.Instance.UIStatus.gameObject.SetActive(true);
        GameManager.Instance.Player.OnStatChanged?.Invoke();        // 상태창 갱신 이벤트 호출
        CloseButtonUI();
    }

    public void OpenInventory()     // 인벤토리 UI 열기
    {
        UIManager.Instance.UIInventory.gameObject.SetActive(true);
        CloseButtonUI();
    }

    public void OpenButtonUI()      // 버튼 UI 활성화
    {
        ButtonUI.SetActive(true);
    }

    public void CloseButtonUI()     // 버튼 UI 비활성화
    {
        ButtonUI.SetActive(false);
    }
    #endregion

    #region 캐릭터 정보 세팅
    public void SetPlayerInfo()     // 플레이어 기본 정보 설정 (이름, 레벨)
    {
        var player = GameManager.Instance.Player;
        nameText.text = player.playerName;
        levelText.text = $"Lv {player.level}";
        SetExpBar();                // 경험치바 갱신
    }

    public void SetExpBar()         // 경험치바 및 텍스트 설정
    {
        var player = GameManager.Instance.Player;
        expText.text = $"{player.exp} / {player.requiredExp}";
        uiBar.fillAmount = (float )player.exp / player.requiredExp;
    }
    #endregion

    #region 아이템 추가 버튼
    public void AddItemBtn()    // 버튼 클릭 시 무작위 아이템 추가
    {
        var randomItem = items[Random.Range(0, items.Length)];
        GameManager.Instance.Player.itemData = randomItem;
        GameManager.Instance.Player.addItem?.Invoke();          // 아이템 추가 이벤트 호출
    }
    #endregion
}
