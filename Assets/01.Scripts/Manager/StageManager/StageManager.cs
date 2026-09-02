using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private StageData currentStage;
    [SerializeField] private Transform[] spawnPoints;

    private void Start()
    {
        LoadStage(currentStage);
    }

    public void LoadStage(StageData stageData)
    {
        currentStage = stageData;

        for (int i = 0; i < currentStage.Enemies.Count; i++)
        {
            EnemyData enemyData = currentStage.Enemies[i];

            Instantiate(enemyData.Prefab, spawnPoints[i].position, Quaternion.identity);
        }
    }
}