using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    public Animator _animator;
    public SpriteRenderer _spriteRenderer;
    public EnemyData _enemyData;
    public int _hp = 0;
    private bool _rewardGiven;

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

    public IEnumerator Turn(Player target)
    {
        bool canAttack = true;

        if(_enemyData.BossType == BossType.Crusher)
        {
            if(_crusherReady)
            {
                Crusher(target);
                canAttack = false;
                yield return new WaitForSeconds(1f);
            }
            if(_crusherDelay >= 0)
            {
                _crusherDelay -= 1;
            }
            else
            {
                canAttack = false;
                CrusherReady(target);
            }
        }

        if(_enemyData.BossType == BossType.WaterBarrier)
        {
            if(_barrierUsed == false && (float)_hp / _enemyData.MaxHP < 0.5f)
            {
                WaterBarrier(target);
            }
        }

        if (canAttack)
        {
            Attack(target);
            yield return new WaitForSeconds(1f);
        }

        if(_enemyData.BossType == BossType.WaterBarrier)
        {
            if(_barrierUsed)
            {
                if(_barrierTimer >= 0)
                {
                    WaterBarrierOff(target);
                }
                else
                {
                    _barrierTimer -= 1;
                }
            }
        }

        if(_enemyData.BossType == BossType.ShadowChase)
        {
            if(ScoreManager.instance.totalScore < 80)
            {
                ShadowChase(target);
            }
        }
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
        return FixedDamage(actualDamage);
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
        {
            _animator.SetBool("isDead", true);

            if (!_rewardGiven)
            {
                _rewardGiven = true;
                moneyManager.Instance.AddGold(_enemyData.RewardGold);
            }
        }
        return actualDamage;
    }

    bool _crusherReady = false;
    int _crusherDelay = 2;
    private void CrusherReady(Player target)
    {
        _crusherReady = true;
        Debug.Log($"{_enemyData.EnemyName} : 분쇄 강타 준비!");

        _spriteRenderer.DOKill();
        _spriteRenderer.DOColor(Color.yellow, 0.1f).SetLoops(2, LoopType.Yoyo);
        return;
    }
    private void Crusher(Player target)
    {
        _crusherReady = false;
        _crusherDelay = 3;
        Debug.Log($"{_enemyData.EnemyName} : 분쇄 강타!");

        _spriteRenderer.DOKill();
        _spriteRenderer.DOColor(Color.yellow, 0.1f).SetLoops(2, LoopType.Yoyo);
        target.Damage(this, 32);
    }

    bool _barrierUsed = false;
    int _barrierAmount = 0;
    int _barrierTimer = 2;
    private void WaterBarrier(Player target)
    {
        _barrierUsed = true;
        _barrierAmount = 60;
        _barrierTimer = 2;

        _spriteRenderer.DOKill();
        _spriteRenderer.DOColor(Color.blue, 0.05f).SetLoops(6, LoopType.Yoyo);
        Debug.Log($"{_enemyData.EnemyName} : 수압 방벽! 60 방벽 생성");
    }

    void WaterBarrierOff(Player target)
    {
        _barrierAmount = 0;
    }    

    private void ShadowChase(Player target)
    {
        Debug.Log($"{_enemyData.EnemyName} : 그림자 추격! 추가 피해 8");
        _spriteRenderer.DOKill();
        _spriteRenderer.DOColor(Color.yellow, 0.1f).SetLoops(2, LoopType.Yoyo);
        target.Damage(this, 8);
    }
}