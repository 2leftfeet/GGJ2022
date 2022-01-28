using UnityEngine;

public class PlayerHealth : Health
{
    public override void ReduceHealth(int amount)
    {
        base.ReduceHealth(amount);
        GameEvents.current.PlayerTakeDamageEvent();
    }

    public override void AddHealth(int amount)
    {
        base.AddHealth(amount);
        GameEvents.current.PlayerAddHealthEvent();
    }
}
