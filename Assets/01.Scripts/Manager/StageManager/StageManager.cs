using System;
using System.Collections;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    [SerializeField] private StageData[] stageData;
    private StageData currentStage;
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Player player;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private ScoreManager scoreManager;

    private Enemy[] enemies;


    private void Start()
    {
        LoadStage(stageData[0]);
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

        StartCoroutine(RunPlayTrun());
    }
    public IEnumerator RunPlayTrun()
    {
        yield return new WaitForSeconds(1f);
        while (true)
        {
            int damage = scoreManager.Damage;

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
                Clear();
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
                Gameover();
                break;
            }
        }
    }

    private void Clear()
    {
        int currentLv = 0;
        for(int i = 0;i < stageData.Length; i++)
        {
            if (stageData[i] == currentStage)
            {
                currentLv = i;
                break;
            }
        }
        int nextLv = currentLv + 1;
        if(nextLv >= stageData.Length)
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            LoadStage(stageData[nextLv]);
        }
    }

    private void Gameover()
    {
        SceneManager.LoadScene("Title");
    }
}