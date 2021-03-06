using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageShare : MonoBehaviour
{
    [SerializeField] float damageShareRange = 10f;
    [SerializeField] DamageShareProjectile damageShareProjectilePhysical;
    [SerializeField] DamageShareProjectile damageShareProjectileWater;
    [SerializeField] DamageShareProjectile damageShareProjectileFire;
    [SerializeField] Health originalHealth;

    public void Start()
    {
        originalHealth = GetComponent<Health>();
    }

    public void ShareDamageInRange(int damageAmount, DamageType damageType)
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, damageShareRange);

        DamageShareProjectile projToUse;
        switch(damageType)
        {
            case DamageType.Water:
                projToUse = damageShareProjectileWater;
                break;
            case DamageType.Fire:
                projToUse = damageShareProjectileFire;
                break;
            case DamageType.Physical:
                projToUse = damageShareProjectilePhysical;
                break;
            default:
                projToUse = damageShareProjectilePhysical;
                break;
        }

        List<Health> healthToIgnore = new List<Health>();
        healthToIgnore.Add(originalHealth);

        foreach(var col in colliders)
        {
            var health = col.GetComponent<Health>();
            if(health && !healthToIgnore.Contains(health))
            {
                if(health.vulnerability == VulnerableTo.All || (int)damageType == (int)health.vulnerability)
                {
                    var projectile = Instantiate(projToUse, transform.position, Quaternion.identity);
                    projectile.damageValue = damageAmount;
                    projectile.target = health;

                    healthToIgnore.Add(health);
                }
            }
            
        }
    }

    public void ShareDamageWithAuthor(int damageAmount, DamageType damageType, Health author)
    {
        DamageShareProjectile projToUse;
        switch(damageType)
        {
            case DamageType.Water:
                projToUse = damageShareProjectileWater;
                break;
            case DamageType.Fire:
                projToUse = damageShareProjectileFire;
                break;
            case DamageType.Physical:
                projToUse = damageShareProjectilePhysical;
                break;
            default:
                projToUse = damageShareProjectilePhysical;
                break;
        }

        var projectile = Instantiate(projToUse, transform.position, Quaternion.identity);
        projectile.damageValue = damageAmount;
        projectile.target = author;
    }
}
