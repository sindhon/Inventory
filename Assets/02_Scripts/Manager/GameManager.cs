using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;    // 싱글톤 인스턴스
    public static GameManager Instance { get { return instance; } }

    public Character Player { get; set; }

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

        SetData("Sin", 1, 0, 3, 5, 5, 10, 2);   // 플레이어 정보 초기화
    }

    public void SetData(string name, int level, int exp, int requiredExp,
                       float attack, float defense, float hp, float crit)
    {
        // Player가 아직 연결되지 않았다면 찾거나 새로 생성
        if (Player == null)
        {
            Player = FindObjectOfType<Character>();

            if (Player == null)
            {
                GameObject playerObj = new GameObject("Player");
                Player = playerObj.AddComponent<Character>();
            }
        }

        // 플레이어 데이터 초기화
        Player.Init(name, level, exp, requiredExp, attack, defense, hp, crit);
    }
}
