using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentDamage : MonoBehaviour
{
    
    BoxCollider myCollider;

    [SerializeField] int damage;
    [SerializeField] float damageInterval = 0.5f;
    [SerializeField] DamageType damageType;

    float damageTimer = 0f;

    void Start()
    {
        myCollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if(damageTimer < damageInterval)
        {
            damageTimer += Time.deltaTime;
            if(damageTimer > damageInterval)
            {
                DoDamage();
                damageTimer = 0.0f;
            }
        }
    }

    void DoDamage()
    {
        var colliders = Physics.OverlapBox(transform.position + myCollider.center, myCollider.size, transform.rotation);
        foreach(var col in colliders)
        {
            var health = col.GetComponent<Health>();
            if(health)
            {
                if(health.vulnerability == VulnerableTo.All || (int)damageType == (int)health.vulnerability)
                {
                    health.ReduceHealth(damage, damageType);
                }
            }

            var damageShare = col.GetComponent<DamageShare>();
            if(damageShare)
            {
                damageShare.ShareDamageInRange(damage, damageType);
            }
        }
    }
}
