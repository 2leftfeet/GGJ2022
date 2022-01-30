using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageShareProjectile : MonoBehaviour
{
    public Health target;
    public int damageValue;
    public int speed;
    public float randomDirStrengthStart = 3f;
    public float randomDirDecreaseSpeed = 1f;
    public DamageType damageType;

    Vector3 randomDir;
    float randomDirStrength;


    void Start()
    {
        randomDir = Random.onUnitSphere;
        randomDirStrength = randomDirStrengthStart;
    }
    void Update()
    {
        if(!target) Destroy(gameObject);

        Vector3 dirToTarget = target.transform.position - transform.position;
        dirToTarget = dirToTarget.normalized;
        transform.Translate(dirToTarget * speed * Time.deltaTime);

        if(randomDirStrength > 0f)
        {
            transform.Translate(randomDir * randomDirStrength * Time.deltaTime);
            randomDirStrength -= randomDirDecreaseSpeed * Time.deltaTime;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        var health = other.GetComponent<Health>();
        if(health && health == target)
        {
            health.ReduceHealth(damageValue, damageType);
            Destroy(gameObject);
        }
    }
}
