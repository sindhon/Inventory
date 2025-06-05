using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    [SerializeField] private UIMainMenu uIMainMenu;
    [SerializeField] private UIStatus uIStatus;
    [SerializeField] private UIInventory uIInventory;

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
}
