using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    public static StageManager Instance { get; private set; }
    public event Action<int> OnTurnStart;
    public event Action OnTurnEnd;
    public event Action OnBattleEnd;

    [SerializeField] private StageData[] stageData;
    private StageData currentStage;

    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private Player player;
    [SerializeField] private Enemy enemyPrefab;

    private Enemy[] enemies;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        int stageIndex = GameSaveManager.Instance.CurrentStageIndex;

        if (stageIndex >= 0 && stageIndex < stageData.Length)
            LoadStage(stageData[stageIndex]);
        else
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
        SaveCurrentStage();

        StartCoroutine(RunPlayTrun());
    }
    private void SaveCurrentStage()
    {
        for (int i = 0; i < stageData.Length; i++)
        {
            if (stageData[i] == currentStage)
            {
                GameSaveManager.Instance.SetStage(i);
                break;
            }
        }
    }
    public Enemy[] GetEnemies()
    {
        return enemies;
    }

    public IEnumerator RunPlayTrun()
    {
        yield return new WaitForSeconds(1f);

        int turn = 0;

        while (true)
        {
            turn++;
            OnTurnStart?.Invoke(turn);

            ScoreManager.instance.Ready();
            while (ScoreManager.instance.isPlaying)
            {
                yield return new WaitForEndOfFrame();
            }

            int damage = ScoreManager.instance.Damage;

            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i] != null && enemies[i]._hp > 0)
                {
                    player.Attack(enemies[i], damage);
                    break;
                }
            }

            bool enemyAllDead = true;

            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i] != null && enemies[i]._hp > 0)
                {
                    enemyAllDead = false;
                    break;
                }
            }

            if (enemyAllDead)
            {
                OnBattleEnd?.Invoke();
                Clear();
                break;
            }

            yield return new WaitForSeconds(3f);

            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i] != null && enemies[i]._hp > 0)
                {
                    enemies[i].Attack(player);
                    yield return new WaitForSeconds(1f);
                }
            }

            OnTurnEnd?.Invoke();

            if (player._hp <= 0)
            {
                OnBattleEnd?.Invoke();
                Gameover();
                break;
            }
        }
    }

    private void Clear()
    {
        int currentLv = 0;

        for (int i = 0; i < stageData.Length; i++)
        {
            if (stageData[i] == currentStage)
            {
                currentLv = i;
                break;
            }
        }

        int nextLv = currentLv + 1;

        if (nextLv >= stageData.Length)
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;

            GameSaveManager.Instance.SetStage(nextSceneIndex);

            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            GameSaveManager.Instance.SetStage(nextLv);
            LoadStage(stageData[nextLv]);
        }
    }

    private void Gameover()
    {
        GameSaveManager.Instance.DeleteSave();
        SceneManager.LoadScene("Title");
    }
}