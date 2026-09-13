using UnityEngine;

public class Player : MonoBehaviour
{
    public Animator _animator;
    public int _maxHp;
    public int _hp;
    public int _defense;
    private void Start()
    {
        _hp = _maxHp;
    }

    public void Attack(Enemy target, int pinAtk)
    {
        _animator.SetTrigger("onAttack");
        target.Damage(pinAtk);
    }
    public void Damage(int enemyAtk)
    {
        int temp;
        temp = enemyAtk - _defense;
        if (temp > 0)
        {
            _hp = temp;
            Debug.Log($"Player {temp}피해");
        }
    }
}