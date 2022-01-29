using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageShareHealth : Health
{
    DamageShare damageShare;

    public void Start()
    {
        damageShare = GetComponent<DamageShare>();
    }

    public override void ReduceHealth(int amount, Health author)
    {
        if(author) damageShare.ShareDamageWithAuthor(amount, author);
        healthAmount -= amount;
        CheckForDeath();
    }
}
