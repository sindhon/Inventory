using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    [SerializeField] private Button BackButton;

    private void Start()
    {
        BackButton.onClick.AddListener(CloseInventoryUI);
    }

    public void CloseInventoryUI()
    {
        gameObject.SetActive(false);
        UIManager.Instance.UIMainMenu.OpenButtonUI();
    }
}
