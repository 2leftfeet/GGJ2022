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

    public override bool ReduceHealth(int amount, DamageType damageType, Health author)
    {
        if(vulnerability != VulnerableTo.All && (int)damageType != (int)vulnerability)
            return false;

        if(author) damageShare.ShareDamageWithAuthor(amount, author);
        healthAmount -= amount;
        CheckForDeath();
        return true;
    }
}
