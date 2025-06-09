using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    public event Action OnInfoChanged;
    public event Action OnStatChanged;

    public Action addItem;
    public ItemData itemData;

    public string playerName { get; private set; }
    public int level { get; private set; }
    public int exp {  get; private set; }
    public int requiredExp { get; private set; }
    public float attack { get; private set; }
    public float defense { get; private set; }
    public float hp { get; private set; }
    public float crit { get; private set; }

    public void Init(string name, int level, int exp, int requiredExp, 
                     float attack, float defense, float hp, float crit)
    {
        playerName = name;
        this.level = level;
        this.exp = exp;
        this.requiredExp = requiredExp;
        this.attack = attack;
        this.defense = defense;
        this.hp = hp;
        this.crit = crit;

        OnInfoChanged?.Invoke();
        OnStatChanged?.Invoke();
    }
}
