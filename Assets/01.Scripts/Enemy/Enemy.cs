using DG.Tweening;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator _animator;
    public SpriteRenderer _spriteRenderer;
    public EnemyData _enemyData;
    public int _hp = 0;
    public int _getAttack
    {
        get
        {
            return Random.Range(_enemyData.MinAttack, _enemyData.MaxAttack+1);
        }
    }

    public void Attack(Player target)
    {
        _animator.SetTrigger("onAttack");
        target.Damage(_getAttack);
    }

    public void Damage(int playerAtk)
    {
        int temp = 0;
        temp = playerAtk - _enemyData.Defense;
        if (temp > 0)
        {
            _hp -= temp;
            _spriteRenderer.DOKill();
            _spriteRenderer.DOColor(Color.red, 0.1f).SetLoops(2, LoopType.Yoyo);
        }

        if(_hp <= 0)
        {
            _animator.SetBool("isDead", true);
        }
    }

    private void Start()
    {
        _hp = _enemyData.MaxHP;
        _animator.runtimeAnimatorController = _enemyData.animatorController;
        _spriteRenderer.flipX = _enemyData.isFlip;
    }

}