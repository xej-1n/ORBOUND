using UnityEngine;

public class ShieldEffect : MonoBehaviour
{
    private ShieldData currentShield;

    private int durability;
    private int barrierCount;

    private bool emergencyUsed;
    private bool chargeReady;
    private bool barrierBlockingAction;

    private int directDamageTotal;

    public int Durability => durability;

    public void SetShield(ShieldData shield)
    {
        currentShield = shield;
        ResetShield();
    }

    public void ResetShield()
    {
        if (currentShield == null)
        {
            durability = 0;
            barrierCount = 0;
            emergencyUsed = false;
            chargeReady = false;
            barrierBlockingAction = false;
            directDamageTotal = 0;
            return;
        }

        durability = currentShield.Durability;
        barrierCount = currentShield.Type == ShieldType.Barrier ? currentShield.BarrierCount : 0;
        emergencyUsed = false;
        chargeReady = false;
        barrierBlockingAction = false;
        directDamageTotal = 0;
    }

    public int Apply(Player player, Enemy attacker, int damage)
    {
        if (player == null || damage <= 0)
            return 0;

        if (currentShield == null)
            return damage;

        if (currentShield.Type == ShieldType.Barrier && barrierBlockingAction)
            return 0;

        bool shieldActive = durability > 0;

        int reducedDamage = damage;

        if (shieldActive)
            reducedDamage = Mathf.FloorToInt(damage * (1f - currentShield.DamageReduction / 100f));

        int durabilityDamage = Mathf.Min(durability, reducedDamage);
        int hpDamage = reducedDamage - durabilityDamage;

        durability -= durabilityDamage;

        HandleShieldEffect(player, attacker, shieldActive, durabilityDamage, hpDamage);

        return hpDamage;
    }

    private void HandleShieldEffect(Player player, Enemy attacker, bool shieldActive, int durabilityDamage, int hpDamage)
    {
        switch (currentShield.Type)
        {
            case ShieldType.Reflect:
                Reflect(player, attacker, shieldActive, durabilityDamage, hpDamage);
                break;

            case ShieldType.Explosion:
                Explosion(player, shieldActive, durabilityDamage);
                break;

            case ShieldType.Charge:
                Charge(shieldActive, durabilityDamage, hpDamage);
                break;

            case ShieldType.Emergency:
                Emergency(player);
                break;
        }
    }

    private void Reflect(Player player, Enemy attacker, bool shieldActive, int durabilityDamage, int hpDamage)
    {
        if (!shieldActive || attacker == null || player._hp <= 0)
            return;

        int actualDamage = durabilityDamage + hpDamage;
        int reflectDamage = Mathf.FloorToInt(actualDamage * currentShield.ReflectPercent);

        if (reflectDamage > 0)
            attacker.FixedDamage(reflectDamage);
    }

    private void Explosion(Player player, bool shieldActive, int durabilityDamage)
    {
        if (!shieldActive || durabilityDamage <= 0 || durability > 0 || player._hp <= 0)
            return;

        if (currentShield.ExplosionDamage <= 0)
            return;

        Enemy[] enemies = StageManager.Instance.GetEnemies();

        foreach (Enemy enemy in enemies)
        {
            if (enemy != null && enemy._hp > 0)
                enemy.Damage(currentShield.ExplosionDamage);
        }
    }

    private void Charge(bool shieldActive, int durabilityDamage, int hpDamage)
    {
        if (!shieldActive)
            return;

        if (durabilityDamage > 0 || hpDamage > 0)
            chargeReady = true;
    }

    private void Emergency(Player player)
    {
        if (emergencyUsed || player._hp <= 0)
            return;

        float hpPercent = (float)player._hp / player._maxHp;

        if (hpPercent > currentShield.EmergencyThreshold)
            return;

        durability = Mathf.Min(
            durability + currentShield.EmergencyDurability,
            currentShield.Durability
        );

        emergencyUsed = true;
    }

    public int GetChargeBonus()
    {
        if (!chargeReady || currentShield == null)
            return 0;

        chargeReady = false;
        return currentShield.ChargeBonusDamage;
    }

    public void BeginEnemyAction()
    {
        if (currentShield == null || currentShield.Type != ShieldType.Barrier)
            return;

        barrierBlockingAction = false;

        if (barrierCount <= 0)
            return;

        barrierCount--;
        barrierBlockingAction = true;
    }

    public void EndEnemyAction()
    {
        barrierBlockingAction = false;
    }

    public void BeginPlayerAttack()
    {
        directDamageTotal = 0;
    }

    public void AddDirectDamage(int damage)
    {
        if (damage <= 0)
            return;

        directDamageTotal += damage;
    }

    public void EndPlayerAttack(Player player)
    {
        if (player == null || currentShield == null)
            return;

        if (currentShield.Type == ShieldType.Lifesteal && durability > 0)
        {
            int heal = Mathf.FloorToInt(directDamageTotal * currentShield.LifestealPercent);
            player.Heal(heal);
        }

        directDamageTotal = 0;
    }

    public void OnTurnStart(Player player, int turn)
    {
        if (player == null || currentShield == null)
            return;

        if (currentShield.Type != ShieldType.Regeneration)
            return;

        if (turn < 2)
            return;

        if (durability <= 0)
            return;

        player.Heal(currentShield.RegenerationAmount);
    }

    public void OnBattleEnd()
    {
        ResetShield();
    }
}