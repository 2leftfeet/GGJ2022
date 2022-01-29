using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageShareProjectile : MonoBehaviour
{
    public Health target;
    public int damageValue;
    public int speed;


    // Update is called once per frame
    void Update()
    {
        Vector3 dirToTarget = target.transform.position - transform.position;
        dirToTarget = dirToTarget.normalized;
        transform.Translate(dirToTarget * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        var health = other.GetComponent<Health>();
        if(health && health == target)
        {
            health.ReduceHealth(damageValue);
            Destroy(gameObject);
        }
    }
}
