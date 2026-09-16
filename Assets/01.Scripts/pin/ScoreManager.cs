using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int totalScore = 0;
    private float turnTimer = 10f;
    private bool isTurnActive = false;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (isTurnActive)
        {
            turnTimer -= Time.deltaTime;
            if (turnTimer <= 0) EndTurn();
        }
    }

    public void StartTurn()
    {
        isTurnActive = true;
        turnTimer = 10f;
        totalScore = 0;
        Debug.Log("공 발사! 10초 턴 시작!");
    }

    public void AddScore(int score)
    {
        if (!isTurnActive) return;

        totalScore += score;
        Debug.Log(score + "점 획득! 현재 점수: " + totalScore);

        if (totalScore >= 200)
        {
            totalScore = 200;
            EndTurn();
        }
    }

    public void EndTurn()
    {
        if (!isTurnActive) return;
        isTurnActive = false;
        int damage = totalScore / 2;
        Debug.Log("턴 종료! 총 점수: " + totalScore + " / 몬스터 데미지: " + damage);
    }
}