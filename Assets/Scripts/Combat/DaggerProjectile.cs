using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerProjectile : MonoBehaviour
{
    [SerializeField] float projectileSpeed;
    [SerializeField] float spinSpeed;
    [SerializeField] LayerMask groundLayer;

    Rigidbody body;

    void Awake()
    {
        body = GetComponent<Rigidbody>();
        body.velocity = transform.forward * projectileSpeed;
        body.angularVelocity = transform.right * spinSpeed;
    }

   /* void OnCollisionEnter(Collision other)
    {
        if( groundLayer == (groundLayer | (1 << other.gameObject.layer)) )
        {
            Physics.IgnoreCollision(GetComponent<Collider>(), other.collider, true);
            body.isKinematic = true;
        }

    }*/
}
