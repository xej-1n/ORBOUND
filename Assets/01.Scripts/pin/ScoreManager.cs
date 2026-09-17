using UnityEngine;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int totalScore = 0;
    private float turnTimer = 10f;
    private bool isTurnActive = false;


    private float lastBottomHitTime = -100f;
    private int comboCount = 0;
    private bool hasGotAllHitBonus = false;
    private List<PegType> hitPegs = new List<PegType>();

    public int Damage { get; private set; }

    void Awake() { instance = this; }

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
        comboCount = 0;
        hasGotAllHitBonus = false;
        hitPegs.Clear();
        lastBottomHitTime = -100f;
    }


    public void AddPegHit(PegType type, int score)
    {
        if (!isTurnActive) return;

        totalScore += score; 


        if (type == PegType.BottomBumper)
        {
            lastBottomHitTime = Time.time;
        }
        else if (type == PegType.TopBumper)
        {
            if (comboCount < 3 && Time.time - lastBottomHitTime <= 3f)
            {
                totalScore += 12;
                comboCount++;
                lastBottomHitTime = -100f;
                Debug.Log("콤보 성공! +12점!");
            }
        }


        if (!hitPegs.Contains(type)) hitPegs.Add(type);

        if (!hasGotAllHitBonus && hitPegs.Count == 4)
        {
            totalScore += 20;
            hasGotAllHitBonus = true;
            Debug.Log("올클리어 보너스! +20점!");
        }

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

  
        float multiplier = GetMultiplier(totalScore);
        Damage = Mathf.FloorToInt((totalScore / 2f) * multiplier);


        Debug.Log("턴 종료! 최종 점수: " + totalScore + " / 몬스터에게 줄 데미지: " + Damage);
    }


    private float GetMultiplier(int score)
    {
        if (score <= 19) return 0.5f;
        if (score <= 49) return 0.7f;
        if (score <= 79) return 0.85f;
        if (score <= 109) return 1.0f;
        if (score <= 139) return 1.5f;
        if (score <= 169) return 2.0f;
        if (score <= 189) return 2.5f;
        return 3.0f;
    }
}