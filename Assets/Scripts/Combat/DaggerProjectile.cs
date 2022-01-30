using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerProjectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed;
    [SerializeField] float spinSpeed;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask affinityLayer;
    [SerializeField] int daggerDamage;
    [SerializeField] GameObject bloodVFX;
    [SerializeField] GameObject fireAffinity;
    [SerializeField] GameObject waterAffinity;
    

    Rigidbody body;
    DamageType currentType = DamageType.Physical;


    public Health author;
    public Vector3 launchPos;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
        body.velocity = transform.forward * projectileSpeed;
        body.angularVelocity = transform.right * spinSpeed;
    }

    void Start()
    {
        Physics.IgnoreCollision(author.GetComponent<Collider>(), GetComponent<Collider>(), true);
    }

    void OnCollisionEnter(Collision other)
    {
        var hitHealth = other.gameObject.GetComponent<Health>();
        if(hitHealth && hitHealth.healthAmount > 0)
        {
            if(hitHealth.ReduceHealth(daggerDamage, currentType, author))
            {
                Instantiate(bloodVFX, other.contacts[0].point + other.contacts[0].normal * 0.2f, Quaternion.FromToRotation(Vector3.back, other.contacts[0].normal));
                Destroy(this.gameObject);
            }
            //if(author) author.ReduceHealth(daggerDamage);
        }

        if( groundLayer == (groundLayer | (1 << other.gameObject.layer)) )
        {
            var sp = author.GetComponent<ShootProjectile>();
            if(sp)
            {
                sp.SpawnPropKnifeAtCollision(other.contacts[0].point, launchPos);
                Destroy(this.gameObject);
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if( affinityLayer == (affinityLayer | (1 << other.gameObject.layer)))
        {
            var envDamage = other.GetComponentInParent<EnvironmentDamage>();
            if(envDamage)
            {
                Debug.Log("OnTriggerEnter");
                currentType = envDamage.damageType;
                if(envDamage.damageType == DamageType.Fire)
                {
                    fireAffinity.SetActive(true);
                    waterAffinity.SetActive(false);
                }
                if(envDamage.damageType == DamageType.Water)
                {
                    fireAffinity.SetActive(false);
                    waterAffinity.SetActive(true);
                }
            }
        }
    }
}
