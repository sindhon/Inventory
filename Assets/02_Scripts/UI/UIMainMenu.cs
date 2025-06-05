using System.Numerics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Experimental.GraphView.GraphView;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject ButtonUI;

    [SerializeField] private Button StatusButton;
    [SerializeField] private Button InventoryButton;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI expText;
    [SerializeField] private Image uiBar;

    private void Start()
    {
        OpenMainMenu();
        SetPlayerInfo();

        StatusButton.onClick.AddListener(OpenStatus);
        InventoryButton.onClick.AddListener(OpenInventory);

        GameManager.Instance.Player.OnInfoChanged += SetPlayerInfo;
    }

    private void OnDisable()
    {
        GameManager.Instance.Player.OnInfoChanged -= SetPlayerInfo;
    }

    #region UI 전환
    public void OpenMainMenu()
    {
        gameObject.SetActive(true);
        OpenButtonUI();
    }

    public void OpenStatus()
    {
        UIManager.Instance.UIStatus.gameObject.SetActive(true);
        CloseButtonUI();
    }

    public void OpenInventory()
    {
        UIManager.Instance.UIInventory.gameObject.SetActive(true);
        CloseButtonUI();
    }

    public void OpenButtonUI()
    {
        ButtonUI.SetActive(true);
    }

    public void CloseButtonUI()
    {
        ButtonUI.SetActive(false);
    }
    #endregion

    #region 캐릭터 정보 세팅
    public void SetPlayerInfo()
    {
        var player = GameManager.Instance.Player;
        nameText.text = player.playerName;
        levelText.text = $"Lv {player.level}";
        SetExpBar();
    }

    public void SetExpBar()
    {
        var player = GameManager.Instance.Player;
        expText.text = $"{player.exp} / {player.requiredExp}";
        uiBar.fillAmount = (float )player.exp / player.requiredExp;
    }
    #endregion
}
