using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
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
    }

    private void Start()
    {
        SetData("Sin", 1, 0, 3, 5, 5, 10, 2);
    }

    public void SetData(string name, int level, int exp, int requiredExp,
                       float attack, float defense, float hp, float crit)
    {
        if (Player == null)
        {
            GameObject playerObj = new GameObject("Player");
            Player = playerObj.AddComponent<Character>();
        }

        Player.Init(name, level, exp, requiredExp, attack, defense, hp, crit);
    }
}
