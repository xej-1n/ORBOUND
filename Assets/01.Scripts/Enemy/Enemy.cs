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
        get { return Random.Range(_enemyData.MinAttack, _enemyData.MaxAttack + 1); }
    }

    private void Start()
    {
        _hp = _enemyData.MaxHP;
        _animator.runtimeAnimatorController = _enemyData.animatorController;
        _spriteRenderer.flipX = _enemyData.isFlip;
    }

    public void Attack(Player target)
    {
        _animator.SetTrigger("onAttack");
        target.Damage(this, _getAttack);
    }

    public int Damage(float damage, float defenseIgnore = 0f)
    {
        int defense = Mathf.FloorToInt(_enemyData.Defense * (1f - defenseIgnore));
        int finalDamage = Mathf.Max(0, Mathf.FloorToInt(damage) - defense);
        int actualDamage = Mathf.Min(finalDamage, _hp);

        if (actualDamage <= 0)
            return 0;

        _hp -= actualDamage;

        _spriteRenderer.DOKill();
        _spriteRenderer.DOColor(Color.red, 0.1f).SetLoops(2, LoopType.Yoyo);

        if (_hp <= 0)
            _animator.SetBool("isDead", true);

        return actualDamage;
    }
    public int FixedDamage(int damage)
    {
        int actualDamage = Mathf.Min(Mathf.Max(0, damage), _hp);

        if (actualDamage <= 0)
            return 0;

        _hp -= actualDamage;

        _spriteRenderer.DOKill();
        _spriteRenderer.DOColor(Color.red, 0.1f).SetLoops(2, LoopType.Yoyo);

        if (_hp <= 0)
            _animator.SetBool("isDead", true);

        return actualDamage;
    }
}