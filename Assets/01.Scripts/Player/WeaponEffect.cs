using UnityEngine;

public class WeaponEffect : MonoBehaviour
{
    private Enemy markedEnemy;
    private int markTurns;

    private Enemy dotTarget;
    private int dotDamage;
    private int dotTurns;

    private Enemy focusTarget;
    private int focusLevel;
    private bool focusLastAttackDealtDamage;

    private void Start()
    {
        StageManager.Instance.OnTurnEnd += HandleTurnEnd;
        StageManager.Instance.OnBattleEnd += HandleBattleEnd;
    }

    private void OnDestroy()
    {
        if (StageManager.Instance == null)
            return;

        StageManager.Instance.OnTurnEnd -= HandleTurnEnd;
        StageManager.Instance.OnBattleEnd -= HandleBattleEnd;
    }

    public void Apply(Player player, Enemy target, int pinAtk, WeaponData weapon)
    {
        if (player == null || target == null || weapon == null || pinAtk <= 0)
            return;

        switch (weapon.Type)
        {
            case WeaponType.Basic:
                Basic(target, pinAtk);
                break;

            case WeaponType.Pierce:
                Pierce(target, pinAtk);
                break;

            case WeaponType.Chain:
                Chain(target, pinAtk);
                break;

            case WeaponType.Explosion:
                Explosion(target, pinAtk);
                break;

            case WeaponType.Split:
                Split(target, pinAtk);
                break;

            case WeaponType.Lifesteal:
                Lifesteal(player, target, pinAtk);
                break;

            case WeaponType.Execute:
                Execute(target, pinAtk);
                break;

            case WeaponType.DamageOverTime:
                DamageOverTime(target, pinAtk);
                break;

            case WeaponType.Mark:
                Mark(target, pinAtk);
                break;

            case WeaponType.Siege:
                Siege(target, pinAtk);
                break;

            case WeaponType.Spread:
                Spread(target, pinAtk);
                break;

            case WeaponType.Focus:
                Focus(target, pinAtk);
                break;
        }
    }

    private int GetDamage(int pinAtk, float multiplier)
    {
        return Mathf.FloorToInt(pinAtk * multiplier);
    }

    private void Basic(Enemy target, int pinAtk)
    {
        target.Damage(GetDamage(pinAtk, 1f));
    }

    private void Pierce(Enemy target, int pinAtk)
    {
        Enemy[] enemies = StageManager.Instance.GetEnemies();

        target.Damage(GetDamage(pinAtk, 0.9f), 0.5f);

        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemy = enemies[i];

            if (enemy == null || enemy == target || enemy._hp <= 0)
                continue;

            enemy.Damage(GetDamage(pinAtk, 0.6f), 0.5f);
            break;
        }
    }

    private void Chain(Enemy target, int pinAtk)
    {
        Enemy[] enemies = StageManager.Instance.GetEnemies();

        target.Damage(GetDamage(pinAtk, 1f));

        int count = 0;

        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemy = enemies[i];

            if (enemy == null || enemy == target || enemy._hp <= 0)
                continue;

            count++;

            if (count == 1)
                enemy.Damage(GetDamage(pinAtk, 0.5f));
            else if (count == 2)
                enemy.Damage(GetDamage(pinAtk, 0.25f));
            else
                break;
        }
    }

    private void Explosion(Enemy target, int pinAtk)
    {
        Enemy[] enemies = StageManager.Instance.GetEnemies();

        target.Damage(GetDamage(pinAtk, 0.9f));

        int count = 0;

        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemy = enemies[i];

            if (enemy == null || enemy == target || enemy._hp <= 0)
                continue;

            enemy.Damage(GetDamage(pinAtk, 0.5f));

            count++;

            if (count >= 2)
                break;
        }
    }

    private void Split(Enemy target, int pinAtk)
    {
        int damage = GetDamage(pinAtk, 0.4f);

        for (int i = 0; i < 3; i++)
        {
            if (target == null || target._hp <= 0)
                break;

            target.Damage(damage);
        }
    }

    private void Lifesteal(Player player, Enemy target, int pinAtk)
    {
        int damage = GetDamage(pinAtk, 0.9f);
        int actualDamage = target.Damage(damage);

        int heal = Mathf.FloorToInt(actualDamage * 0.2f);

        player.Heal(heal);
    }

    private void Execute(Enemy target, int pinAtk)
    {
        float hpRatio = (float)target._hp / target._enemyData.MaxHP;
        float multiplier = 0.9f * (1f + 0.5f * (1f - hpRatio));

        target.Damage(GetDamage(pinAtk, multiplier));
    }

    private void DamageOverTime(Enemy target, int pinAtk)
    {
        int directDamage = GetDamage(pinAtk, 0.8f);
        int actualDamage = target.Damage(directDamage);

        if (actualDamage <= 0 || target._hp <= 0)
            return;

        dotTarget = target;
        dotDamage = GetDamage(pinAtk, 0.25f);
        dotTurns = 2;
    }

    private void Mark(Enemy target, int pinAtk)
    {
        bool isMarked = markedEnemy == target && markTurns > 0;

        int damage = GetDamage(pinAtk, 0.9f);

        if (isMarked)
            damage += GetDamage(pinAtk, 0.4f);

        int actualDamage = target.Damage(damage);

        markedEnemy = null;
        markTurns = 0;

        if (target._hp > 0 && actualDamage > 0)
        {
            markedEnemy = target;
            markTurns = 2;
        }
    }

    private void Siege(Enemy target, int pinAtk)
    {
        int defense = target._enemyData.Defense;
        int bonusDamage = Mathf.Min(defense * 2, 12);

        target.Damage(pinAtk + bonusDamage);
    }

    private void Spread(Enemy target, int pinAtk)
    {
        Enemy[] enemies = StageManager.Instance.GetEnemies();

        int damage = GetDamage(pinAtk, 0.6f);

        target.Damage(damage);

        int count = 0;

        for (int i = 0; i < enemies.Length; i++)
        {
            Enemy enemy = enemies[i];

            if (enemy == null || enemy == target || enemy._hp <= 0)
                continue;

            enemy.Damage(damage);

            count++;

            if (count >= 2)
                break;
        }
    }

    private void Focus(Enemy target, int pinAtk)
    {
        if (focusTarget != target || !focusLastAttackDealtDamage)
        {
            focusTarget = target;
            focusLevel = 0;
        }
        else
        {
            focusLevel = Mathf.Min(focusLevel + 1, 3);
        }

        float multiplier = 1f + focusLevel * 0.2f;

        int damage = GetDamage(pinAtk, multiplier);
        int actualDamage = target.Damage(damage);

        focusLastAttackDealtDamage = actualDamage > 0;
    }

    private void HandleTurnEnd()
    {
        ApplyDot();
        UpdateMark();
    }

    private void HandleBattleEnd()
    {
        ResetEffects();
    }

    private void ApplyDot()
    {
        if (dotTarget == null || dotTurns <= 0)
            return;

        if (dotTarget._hp > 0)
            dotTarget.Damage(dotDamage);

        dotTurns--;

        if (dotTurns <= 0 || dotTarget._hp <= 0)
        {
            dotTarget = null;
            dotDamage = 0;
            dotTurns = 0;
        }
    }

    private void UpdateMark()
    {
        if (markedEnemy == null)
            return;

        markTurns--;

        if (markTurns <= 0 || markedEnemy._hp <= 0)
        {
            markedEnemy = null;
            markTurns = 0;
        }
    }

    private void ResetEffects()
    {
        markedEnemy = null;
        markTurns = 0;

        dotTarget = null;
        dotDamage = 0;
        dotTurns = 0;

        focusTarget = null;
        focusLevel = 0;
        focusLastAttackDealtDamage = false;
    }
}