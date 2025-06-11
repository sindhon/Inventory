using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatus : MonoBehaviour
{
    [SerializeField] private Button backButton;             // 메인 메뉴로 돌아가는 버튼
    [SerializeField] private TextMeshProUGUI attackText;    // 플레이어 공격력
    [SerializeField] private TextMeshProUGUI defenseText;   // 플레이어 방어력
    [SerializeField] private TextMeshProUGUI hpText;        // 플레이어 체력
    [SerializeField] private TextMeshProUGUI critText;      // 플레이어 치명타 확률

    private void Start()
    {
        SetPlayerInfo();    // 플레이어 스텟 UI 갱신

        backButton.onClick.AddListener(CloseStatusUI);

        GameManager.Instance.Player.OnStatChanged += SetPlayerInfo; // 플레이어 스텟 변경 시 UI 갱신 이벤트 구독
    }

    public void CloseStatusUI()     // 스텟 UI 비활성화 및 메인 메뉴 UI 활성화
    {
        gameObject.SetActive(false);
        UIManager.Instance.UIMainMenu.OpenButtonUI();
    }

    #region 캐릭터 정보 세팅
    public void SetPlayerInfo()     // 플레이어 기본 스텟 설정
    {
        var player = GameManager.Instance.Player;
        attackText.text = player.attack.ToString();
        defenseText.text = player.defense.ToString();
        hpText.text = player.hp.ToString();
        critText.text = player.crit.ToString();
    }
    #endregion
}
