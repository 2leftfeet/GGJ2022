using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Health author;
    public int projectileDamage;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] GameObject iceVFX;
    [SerializeField] GameObject audio;

    private void Start()
    {
        Physics.IgnoreCollision(author.GetComponent<Collider>(), GetComponent<Collider>(), true);
    }

    void OnTriggerEnter(Collider other)
    {

        if (groundLayer == (groundLayer | (1 << other.gameObject.layer)))
        {
            if(audio)
                Destroy(Instantiate(audio, this.transform.position, this.transform.rotation), 1f);
            Destroy(this.gameObject);
        }

        var health = other.GetComponent<Health>();
        if (health && health != author)
        {
            health.ReduceHealth(projectileDamage);
            Destroy(this.gameObject);
            if (audio)
                Destroy(Instantiate(audio, this.transform.position, this.transform.rotation), 1f);
        }

    }

}
