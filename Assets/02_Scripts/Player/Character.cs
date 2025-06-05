using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float attack;
    [SerializeField] private float defense;
    [SerializeField] private float hp;
    [SerializeField] private float crit;

    public Character(float attack, float defense, float hp, float crit)
    {
        this.attack = attack;
        this.defense = defense;
        this.hp = hp;
        this.crit = crit;
    }
}
