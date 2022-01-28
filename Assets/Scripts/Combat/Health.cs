using UnityEngine;

public class Health : MonoBehaviour
{
    public int healthAmount;

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

    void CheckForDeath()
    {
        if (healthAmount <= 0)
        {
            Debug.Log("I'm Dead");
        }
    }
}
