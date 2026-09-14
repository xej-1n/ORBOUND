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
    public WeaponData _weapon;
    public ShieldData _shield;

    private void Start()
    {
        _hp = _maxHp;
    }

    public void Attack(Enemy target, int pinAtk)
    {
        if (_weapon != null)
        {
            pinAtk = (int)(pinAtk * _weapon.DamageMultiplier);
            pinAtk += (int)_weapon.BonusDamage;
        }
        //공격무기 효과 추가 및 방어력 무시 추가해야함

        _animator.SetTrigger("onAttack");
        target.Damage(pinAtk);
    }
    public void Damage(int enemyAtk)
    {
        int defense = _defense;
        if (_shield != null)
            defense += (int)_shield.ShieldAmount;

        int temp;
        temp = enemyAtk - defense;
        if(_shield != null)
        {
            temp = (int)(temp * (100 - _shield.DamageReduction) / 100);
        }
        // 실드 효과 추가해야함
        if (temp > 0)
        {
            _hp -= temp;
            _spriteRenderer.DOKill();
            _spriteRenderer.DOColor(Color.red, 0.1f).SetLoops(2, LoopType.Yoyo);
            _hpSlider.value = (float)_hp / _maxHp;
            Debug.Log($"Player {temp}피해");
        }

        if (_hp <= 0)
        {
            _animator.SetBool("isDead", true);
        }
    }
}