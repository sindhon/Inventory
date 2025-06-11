using System;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Action OnInfoChanged;    // 플레이어 정보(이름, 레벨) 변경 시 호출되는 이벤트
    public Action OnStatChanged;    // 플레이어 스텟 변경 시 호출되는 이벤트

    public Action addItem;          // 아이템 추가 시 추가되는 이벤트
    public ItemData itemData;       // 아이템 데이터

    public string playerName { get; private set; }  // 플레이어 이름
    public int level { get; private set; }          // 레벨
    public int exp {  get; private set; }           // 경험치
    public int requiredExp { get; private set; }    // 레벨업을 위한 필요 경험치
    public float attack { get; private set; }       // 공격력
    public float defense { get; private set; }      // 방어력
    public float hp { get; private set; }           // 체력
    public float crit { get; private set; }         // 치명타 확률

    public void Init(string name, int level, int exp, int requiredExp, 
                     float attack, float defense, float hp, float crit)     // 플레이어 정보 초기화
    {
        playerName = name;
        this.level = level;
        this.exp = exp;
        this.requiredExp = requiredExp;
        this.attack = attack;
        this.defense = defense;
        this.hp = hp;
        this.crit = crit;

        // 초기화 후 UI 등을 갱신하기 위해 이벤트 호출
        OnInfoChanged?.Invoke();
        OnStatChanged?.Invoke();
    }


    #region 장비 장착/해제 시 스탯 변경 함수
    public void AddAttack(float value) => attack += value;
    public void RemoveAttack(float value) => attack -= value;

    public void AddDefense(float value) => defense += value;
    public void RemoveDefense(float value) => defense -= value;

    public void AddHp(float value) => hp += value;
    public void RemoveHp(float value) => hp -= value;

    public void AddCrit(float value) => crit += value;
    public void RemoveCrit(float value) => crit -= value;
    #endregion
}
