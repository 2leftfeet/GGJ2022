using UnityEngine;

public enum DamageType{
    Water,
    Fire,
    Physical
};

public enum VulnerableTo{
    Water,
    Fire,
    All
}

public class Health : MonoBehaviour
{
    public int healthAmount;
    IDeadable owner;

    public VulnerableTo vulnerability;

    void Awake()
    {
        owner = GetComponent<IDeadable>();
    }

    public virtual void ReduceHealth(int amount)
    {
        ReduceHealth(amount, null);
    }

    public virtual void ReduceHealth(int amount, Health author)
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
