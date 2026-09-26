using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public Animator _animator;
    public int _maxHp;
    public int _hp;
    public SpriteRenderer _spriteRenderer;
    public int _defense;
    public Slider _hpSlider;

    [SerializeField] private WeaponEffect _weaponEffect;
    [SerializeField] private ShieldEffect _shieldEffect;

    [SerializeField] private Image _weaponIcon;
    [SerializeField] private Image _shieldIcon;

    public AudioClip _attackSound;
    public AudioClip _deadSound;

    private void Start()
    {
        _hp = _maxHp;
        _hpSlider.value = 1f;

        if(_weaponEffect != null)
        {
            WeaponData equippedWeapon = EquipManager.Instance.GetEquippedWeapon();
            _weaponEffect.SetWeapon(equippedWeapon);
            if(equippedWeapon != null)
            {
                _weaponIcon.enabled = true;
                _weaponIcon.sprite = equippedWeapon.Icon;
            }
            else
            {
                _weaponIcon.enabled = false;
                _weaponIcon.sprite = null;
            }
        }

        if (_shieldEffect != null)
        {
            ShieldData equippedShield = EquipManager.Instance.GetEquippedShield();
            _shieldEffect.SetShield(equippedShield);
            if(equippedShield != null)
            {
                _shieldIcon.enabled = true;
                _shieldIcon.sprite =  equippedShield.Icon;
            }
            else
            {
                _shieldIcon.enabled = false;
                _shieldIcon.sprite = null;
            }
        }

        if (StageManager.Instance != null)
        {
            StageManager.Instance.OnTurnStart += HandleTurnStart;
            StageManager.Instance.OnBattleEnd += HandleBattleEnd;
        }
    }

    private void OnDestroy()
    {
        if (StageManager.Instance == null)
            return;

        StageManager.Instance.OnTurnStart -= HandleTurnStart;
        StageManager.Instance.OnBattleEnd -= HandleBattleEnd;
    }

    public void Attack(Enemy target, int pinAtk)
    {
        if (target == null || pinAtk <= 0)
            return;

        _animator.SetTrigger("onAttack");
        SoundManager.Instance.PlaySFX(_attackSound);

        if (_shieldEffect != null)
            pinAtk += _shieldEffect.GetChargeBonus();

        if (_shieldEffect != null)
            _shieldEffect.BeginPlayerAttack();

        Enemy[] enemies = StageManager.Instance != null ? StageManager.Instance.GetEnemies() : null;

        int[] beforeHp = null;

        if (enemies != null)
        {
            beforeHp = new int[enemies.Length];

            for (int i = 0; i < enemies.Length; i++)
                beforeHp[i] = enemies[i] != null ? enemies[i]._hp : 0;
        }

        if (_weaponEffect.HasWeapon())
            _weaponEffect.Apply(this, target, pinAtk);
        else
            target.Damage(pinAtk);

        if (_shieldEffect != null && enemies != null && beforeHp != null)
        {
            for (int i = 0; i < enemies.Length; i++)
            {
                if (enemies[i] == null)
                    continue;

                int actualDamage = Mathf.Max(0, beforeHp[i] - enemies[i]._hp);
                _shieldEffect.AddDirectDamage(actualDamage);
            }

            _shieldEffect.EndPlayerAttack(this);
        }
    }

    public void Damage(Enemy attacker, int enemyAtk)
    {
        int damage = enemyAtk;

        if (_shieldEffect != null)
            damage = _shieldEffect.Apply(this, attacker, enemyAtk);

        if (damage <= 0)
            return;

        _hp -= damage;
        _hp = Mathf.Max(0, _hp);

        _spriteRenderer.DOKill();
        _spriteRenderer.DOColor(Color.red, 0.1f).SetLoops(2, LoopType.Yoyo);

        _hpSlider.value = (float)_hp / _maxHp;

        if (_hp <= 0)
        {
            SoundManager.Instance.PlaySFX(_deadSound);
            _animator.SetBool("isDead", true);
        }
    }

    public void Damage(int enemyAtk)
    {
        Damage(null, enemyAtk);
    }

    public void Heal(int amount)
    {
        if (amount <= 0)
            return;

        _hp = Mathf.Min(_hp + amount, _maxHp);
        _hpSlider.value = (float)_hp / _maxHp;
    }

    private void HandleTurnStart(int turn)
    {
        if (_shieldEffect != null)
            _shieldEffect.OnTurnStart(this, turn);
    }

    private void HandleBattleEnd()
    {
        if (_shieldEffect != null)
            _shieldEffect.OnBattleEnd();
    }

    public void BeginEnemyAction()
    {
        if (_shieldEffect != null)
            _shieldEffect.BeginEnemyAction();
    }

    public void EndEnemyAction()
    {
        if (_shieldEffect != null)
            _shieldEffect.EndEnemyAction();
    }
}