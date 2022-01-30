using UnityEngine;

public class PlayerHealth : Health
{
    public override bool ReduceHealth(int amount, DamageType damageType, Health author)
    {
        base.ReduceHealth(amount, damageType, author);
        GameEvents.current.PlayerTakeDamageEvent();

        return true;
    }

    public override void AddHealth(int amount)
    {
        base.AddHealth(amount);
        if(GameEvents.current) GameEvents.current.PlayerAddHealthEvent();
    }

    public override void CheckForDeath()
    {
        if (healthAmount <= 0)
        {
            if(GameEvents.current) GameEvents.current.PlayerDeathEvent();
        }
    }
}
