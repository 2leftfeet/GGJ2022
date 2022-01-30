using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageShareHealth : Health
{
    DamageShare damageShare;
    public GameObject fireEffect;


    public void Start()
    {
        damageShare = GetComponent<DamageShare>();
    }

    public override bool ReduceHealth(int amount, DamageType damageType, Health author)
    {
        if(vulnerability != VulnerableTo.All && (int)damageType != (int)vulnerability)
            return false;

        if(vulnerability == VulnerableTo.Water)
        {
            vulnerability = VulnerableTo.All;
            if(fireEffect) fireEffect.SetActive(false);
        }

        if(author) damageShare.ShareDamageWithAuthor(amount, damageType, author);
        healthAmount -= amount;
        CheckForDeath();
        return true;
    }
}
