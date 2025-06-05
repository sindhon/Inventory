using UnityEngine;
using UnityEngine.UI;

public class UIStatus : MonoBehaviour
{
    [SerializeField] private Button BackButton;

    private void Start()
    {
        BackButton.onClick.AddListener(CloseStatusUI);
    }

    public void CloseStatusUI()
    {
        gameObject.SetActive(false);
        UIManager.Instance.UIMainMenu.OpenButtonUI();
    }
}
