using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class StageManager : MonoBehaviour
{
    [SerializeField] private StageData currentStage;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Player player;
    [SerializeField] private Enemy enemyPrefab;

    private Enemy[] enemies;


    private void Start()
    {
        LoadStage(currentStage);
        StartCoroutine(RunPlayTrun());
    }

    public void LoadStage(StageData stageData)
    {
        currentStage = stageData;
        enemies = new Enemy[currentStage.Enemies.Count];

        for (int i = 0; i < currentStage.Enemies.Count; i++)
        {
            EnemyData enemyData = currentStage.Enemies[i];

            enemies[i] = Instantiate(enemyPrefab, spawnPoints[i].position, Quaternion.identity);
            if (enemies[i] != null)
                enemies[i]._enemyData = enemyData;
        }
    }
    public IEnumerator RunPlayTrun()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            int damage = 50;
            //핀볼 해야함(임시 데미지 50으로 계산)

            for (int i = 0; i < currentStage.Enemies.Count; i++)
            {
                if (enemies[i]._hp > 0)
                {
                    player.Attack(enemies[i], damage);
                    break;
                }
            }

            bool enemyAllDead = true;
            for(int i = 0; i < currentStage.Enemies.Count; i++)
            {
                if (enemies[i]._hp > 0)
                {
                    enemyAllDead = false;
                    break;
                }
            }
            if(enemyAllDead)
            {
                //승리처리
                break; 
            }

            yield return new WaitForSeconds(3f);

            for (int i = 0; i < currentStage.Enemies.Count; i++)
            {
                if (enemies[i]._hp > 0)
                {
                    enemies[i].Attack(player);
                    yield return new WaitForSeconds(1f);
                }
            }
            if (player._hp <= 0)
            {
                //게임오버처리
                break;
            }
        }
    }
}