using UnityEngine;

public class Enemy : MonoBehaviour
{
    public EnemyData _enemyData;
    public int _hp = 0;
    public int _getAttack
    {
        get
        {
            return Random.Range(_enemyData.MinAttack, _enemyData.MaxAttack+1);
        }
    }

    public void Damage(int playerAtk)
    {
        int temp = 0;
        temp = playerAtk - _enemyData.Defense;
        if (temp > 0)
            _hp -= temp;
    }

    private void Start()
    {
        _hp = _enemyData.MaxHP;
    }

}