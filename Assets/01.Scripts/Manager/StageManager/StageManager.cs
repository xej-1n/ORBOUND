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
        SoundManager.Instance.PlayBGM(currentStage._bgm);

        if(enemies != null)
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy != null)
                    Destroy(enemy.gameObject);
            }
        }

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
    private int CurrentStageIndex => Array.IndexOf(stageData, currentStage);

    private void SaveBattle(int turn, int nextEnemyIndex)
    {
        BattleSaveData data = new BattleSaveData
        {
            turn = turn,
            nextEnemyIndex = nextEnemyIndex,
            score = ScoreManager.instance.totalScore
        };
        player.CaptureState(data, enemies);
        foreach (Enemy enemy in enemies)
            data.enemies.Add(enemy != null ? enemy.CaptureState() : new EnemySaveData());
        GameSaveManager.Instance.SaveBattle(CurrentStageIndex, data);
    }

    private bool FinishBattleIfNeeded()
    {
        if (player._hp <= 0)
        {
            OnBattleEnd?.Invoke();
            Gameover();
            return true;
        }

        foreach (Enemy enemy in enemies)
            if (enemy != null && enemy._hp > 0) return false;

        OnBattleEnd?.Invoke();
        Clear();
        return true;
    }
    public Enemy[] GetEnemies()
    {
        return enemies;
    }

    public IEnumerator RunPlayTrun()
    {
        // Restore after Player.Start and Enemy.Start initialize HP and equipment.
        yield return null;
        int turn = 0;
        int nextEnemyIndex = -1;
        bool resumeShot = false;
        BattleSaveData saved = GameSaveManager.Instance.GetBattle(CurrentStageIndex);
        if (saved != null && saved.enemies != null && saved.enemies.Count == enemies.Length)
        {
            for (int i = 0; i < enemies.Length; i++)
                if (enemies[i] != null && saved.enemies[i] != null)
                    enemies[i].RestoreState(saved.enemies[i]);
            player.RestoreState(saved, enemies);
            turn = Mathf.Max(1, saved.turn);
            nextEnemyIndex = Mathf.Clamp(saved.nextEnemyIndex, -1, enemies.Length);
            ScoreManager.instance.totalScore = saved.score;
            resumeShot = nextEnemyIndex == -1;
        }
        else if (GameSaveManager.Instance.SavedPlayerHp.HasValue)
        {
            player.RestoreHealth(GameSaveManager.Instance.SavedPlayerHp.Value);
        }

        if (FinishBattleIfNeeded()) yield break;
        yield return new WaitForSeconds(1f);

        while (true)
        {
            if (nextEnemyIndex == -1)
            {
                // Do not apply regeneration twice when resuming a saved shot.
                if (!resumeShot)
                {
                    turn++;
                    OnTurnStart?.Invoke(turn);
                }
                resumeShot = false;
                SaveBattle(turn, -1);
                MessageManager.instance.Open("플레이어의 턴", 3f);
                ScoreManager.instance.Ready();
                while (ScoreManager.instance.isPlaying)
                    yield return new WaitForEndOfFrame();

                MessageManager.instance.Open("플레이어의 공격", 1f);
                yield return new WaitForSeconds(1f);
                int damage = ScoreManager.instance.Damage;
                foreach (Enemy enemy in enemies)
                {
                    if (enemy != null && enemy._hp > 0)
                    {
                        player.Attack(enemy, damage);
                        break;
                    }
                }

                if (FinishBattleIfNeeded()) yield break;
                nextEnemyIndex = 0;
                SaveBattle(turn, nextEnemyIndex);
                yield return new WaitForSeconds(3f);
            }

            for (int i = nextEnemyIndex; i < enemies.Length; i++)
            {
                if (enemies[i] != null && enemies[i]._hp > 0)
                    yield return enemies[i].Turn(player);

                if (FinishBattleIfNeeded()) yield break;
                SaveBattle(turn, i + 1);
            }

            OnTurnEnd?.Invoke();
            if (FinishBattleIfNeeded()) yield break;
            nextEnemyIndex = -1;
        }
    }

    private void Clear()
    {
        // Keep HP changes from the finishing attack when moving to the next stage.
        SaveBattle(0, -1);
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

            string nextScenePath = SceneUtility.GetScenePathByBuildIndex(nextSceneIndex);
            if (string.IsNullOrEmpty(nextScenePath))
            {
                Debug.LogError("다음 씬이 빌드 설정에 없습니다: " + nextSceneIndex);
                return;
            }

            string nextSceneName = System.IO.Path.GetFileNameWithoutExtension(nextScenePath);
            GameSaveManager.Instance.SetStage(0, nextSceneName, resetPlayerHealth: true);

            SceneManager.LoadScene(nextSceneIndex);
        }
        else
        {
            LoadStage(stageData[nextLv]);
        }
    }

    private void Gameover()
    {
        GameSaveManager.Instance.DeleteSave();
        SceneManager.LoadScene("Title");
    }
}
