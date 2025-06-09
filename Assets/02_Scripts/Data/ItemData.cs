using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemStatType
{
    Hp,
    Attack,
    Defense,
    Crit
}

public enum ItemType
{
    Atk,
    Def
}

[System.Serializable]
public class ItemStat
{
    public ItemStatType type;   // 아이템의 스텟 종류
    public float value;         // 상승하는 스텟 값
}

[CreateAssetMenu(fileName = "Item", menuName = "New Item")]
public class ItemData : ScriptableObject
{
    [Header("Info")]
    public string itemName;     // 아이템 이름
    public string description;  // 아이템 설명
    public Sprite icon;         // 아이템 아이콘

    [Header("ItemType")]
    public ItemType type;       // 아이템 타입 (무기 / 방어구)

    [Header("ItemStat")]
    public ItemStat[] stats;    // 장비 시 상승하는 스텟
}
