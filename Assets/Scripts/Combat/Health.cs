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

    void CheckForDeath()
    {
        if (healthAmount <= 0)
        {
            owner.OnDeath();
        }
    }
}
