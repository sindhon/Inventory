using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance { get { return instance; } }

    [SerializeField] private GameObject UIMainMenu;
    [SerializeField] private GameObject UIStatus;
    [SerializeField] private GameObject UIInventory;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
}
