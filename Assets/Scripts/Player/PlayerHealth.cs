using UnityEngine;

public class PlayerHealth : Health
{
    public override void ReduceHealth(int amount)
    {
        base.ReduceHealth(amount);
        GameEvents.current.PlayerTakeDamageEvent();
    }
}
