using UnityEngine;

public class BossEffect : MonoBehaviour
{
    private Enemy _enemy;

    private bool _crusherReady;

    private bool _barrierUsed;
    private int _barrierAmount;
    private int _barrierTurns;

    private void Awake()
    {
        _enemy = GetComponent<Enemy>();
    }

    public bool IsBoss()
    {
        return _enemy != null && _enemy._enemyData.IsBoss;
    }

    public bool TryAttack(Player target)
    {
        if (!IsBoss())
            return false;

        switch (_enemy._enemyData.BossType)
        {
            case BossType.Crusher:
                Crusher(target);
                break;

            case BossType.WaterBarrier:
                WaterBarrier(target);
                break;

            case BossType.ShadowChase:
                ShadowChase(target);
                break;

            default:
                return false;
        }

        return true;
    }

    private void Crusher(Player target)
    {
        if (!_crusherReady)
        {
            _crusherReady = true;
            Debug.Log($"{_enemy._enemyData.EnemyName} : 분쇄 강타 준비!");
            return;
        }

        _crusherReady = false;
        Debug.Log($"{_enemy._enemyData.EnemyName} : 분쇄 강타!");

        target.Damage(_enemy, 32);
    }

    private void WaterBarrier(Player target)
    {
        if (!_barrierUsed && _enemy._hp <= 225)
        {
            _barrierUsed = true;
            _barrierAmount = 60;
            _barrierTurns = 2;

            Debug.Log($"{_enemy._enemyData.EnemyName} : 수압 방벽! 60 방벽 생성");
            return;
        }

        target.Damage(_enemy, _enemy._getAttack);
    }

    private void ShadowChase(Player target)
    {
        target.Damage(_enemy, _enemy._getAttack);

        if (ScoreManager.instance != null && ScoreManager.instance.totalScore < 80)
        {
            Debug.Log($"{_enemy._enemyData.EnemyName} : 그림자 추격! 추가 피해 8");
            target.Damage(_enemy, 8);
        }
    }

    public int ApplyDamage(int damage)
    {
        if (_barrierAmount <= 0)
            return damage;

        int barrierDamage = Mathf.Min(damage, _barrierAmount);

        _barrierAmount -= barrierDamage;
        damage -= barrierDamage;

        Debug.Log($"{_enemy._enemyData.EnemyName} : 방벽이 {barrierDamage} 피해 흡수");

        if (_barrierAmount <= 0)
        {
            _barrierAmount = 0;
            _barrierTurns = 0;
            Debug.Log($"{_enemy._enemyData.EnemyName} : 수압 방벽 파괴!");
        }

        return damage;
    }

    public void OnTurnEnd()
    {
        if (_barrierTurns <= 0)
            return;

        _barrierTurns--;

        if (_barrierTurns <= 0)
        {
            _barrierAmount = 0;
            Debug.Log($"{_enemy._enemyData.EnemyName} : 수압 방벽 종료");
        }
    }
}