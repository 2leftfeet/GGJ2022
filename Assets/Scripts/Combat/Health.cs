using UnityEngine;

public class Health : MonoBehaviour
{
    public int healthAmount;
    public IDeadable owner;

    public virtual void ReduceHealth(int amount)
    {
        healthAmount -= amount;
        CheckForDeath();
    }

    public virtual void AddHealth(int amount)
    {
        healthAmount += amount;
        if (healthAmount > 100) healthAmount = 100;
    }

    public virtual void CheckForDeath()
    {
        if (healthAmount <= 0)
        {
            owner.OnDeath();
        }
    }
}
