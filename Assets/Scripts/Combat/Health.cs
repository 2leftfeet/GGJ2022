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

    public virtual bool ReduceHealth(int amount)
    {
        return ReduceHealth(amount, DamageType.Physical, null);
    }

     public virtual bool ReduceHealth(int amount, Health author)
    {
        return ReduceHealth(amount, DamageType.Physical, author);
    }

    public virtual bool ReduceHealth(int amount, DamageType damageType)
    {
        return ReduceHealth(amount, damageType ,null);
    }

    public virtual bool ReduceHealth(int amount, DamageType damageType, Health author)
    {
        if(vulnerability != VulnerableTo.All && (int)damageType != (int)vulnerability)
            return false;

        healthAmount -= amount;
        CheckForDeath();
        return true;
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
