using UnityEngine;

public class Health : MonoBehaviour
{
    public int healthAmount;

    public virtual void ReduceHealth(int amount)
    {
        healthAmount -= amount;
        CheckForDeath();
    }

    void CheckForDeath()
    {
        if (healthAmount <= 0)
        {
            Debug.Log("I'm Dead");
        }
    }
}
