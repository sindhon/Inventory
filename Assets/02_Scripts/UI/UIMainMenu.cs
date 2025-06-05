using UnityEngine;
using UnityEngine.UI;

public class UIMainMenu : MonoBehaviour
{
    [SerializeField] private GameObject ButtonUI;

    [SerializeField] private Button StatusButton;
    [SerializeField] private Button InventoryButton;

    private void Start()
    {
        OpenMainMenu();

        StatusButton.onClick.AddListener(OpenStatus);
        InventoryButton.onClick.AddListener(OpenInventory);
    }

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
}
