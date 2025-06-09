using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIStatus : MonoBehaviour
{
    [SerializeField] private Button backButton;
    [SerializeField] private TextMeshProUGUI attackText;
    [SerializeField] private TextMeshProUGUI defenseText;
    [SerializeField] private TextMeshProUGUI hpText;
    [SerializeField] private TextMeshProUGUI critText;

    private void Start()
    {
        SetPlayerInfo();

        backButton.onClick.AddListener(CloseStatusUI);

        GameManager.Instance.Player.OnStatChanged += SetPlayerInfo;
    }

    public void CloseStatusUI()
    {
        gameObject.SetActive(false);
        UIManager.Instance.UIMainMenu.OpenButtonUI();
    }

    #region 캐릭터 정보 세팅
    public void SetPlayerInfo()
    {
        var player = GameManager.Instance.Player;
        attackText.text = player.attack.ToString();
        defenseText.text = player.defense.ToString();
        hpText.text = player.hp.ToString();
        critText.text = player.crit.ToString();
    }
    #endregion
}
