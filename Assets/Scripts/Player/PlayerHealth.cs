using UnityEngine;

public class PlayerHealth : Health
{
    public override void ReduceHealth(int amount)
    {
        base.ReduceHealth(amount);
        if(GameEvents.current) GameEvents.current.PlayerTakeDamageEvent();
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
