using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Health author;
    public int projectileDamage;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] GameObject iceVFX;

    private void Start()
    {
        Physics.IgnoreCollision(author.GetComponent<Collider>(), GetComponent<Collider>(), true);
    }

    void OnTriggerEnter(Collider other)
    {

        if (groundLayer == (groundLayer | (1 << other.gameObject.layer)))
        {
            Destroy(this.gameObject);
        }

        var health = other.GetComponent<Health>();
        if (health && health != author)
        {
            health.ReduceHealth(projectileDamage);
            Destroy(this.gameObject);
        }

    }

}
