using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSeekerAI : MonoBehaviour, IDeadable
{
    Transform playerT;
    Rigidbody body;
    float damageCooldownTimer = 0f;
    bool canAttack;

    [SerializeField] float flySpeed = 5f;
    [SerializeField] float noiseForceStrength = 1f;
    [SerializeField] float damageRange = 1f;
    [SerializeField] float damageCooldown = 2f;
    [SerializeField] int damageAmount = 30;
    [SerializeField] float damagePushForce = 50f;

    bool isActive = true;


    void Start()
    {
        playerT = FindObjectOfType<PlayerMovement>().transform;
        body = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if(isActive)
        {
            Vector3 dirToPlayer = playerT.position - transform.position;
            Vector3 flyDir = dirToPlayer.normalized;

            flyDir = flyDir * flySpeed;

            if(canAttack && dirToPlayer.magnitude < damageRange)
            {
                canAttack = false;
                playerT.GetComponent<Health>().ReduceHealth(damageAmount);
                playerT.GetComponent<Rigidbody>().AddForce(dirToPlayer * damagePushForce);
                playerT.GetComponent<Rigidbody>().AddForce(Vector3.up * damagePushForce);
            }

            float noiseX = Mathf.PerlinNoise(0.5f, Time.time);
            float noiseY = Mathf.PerlinNoise(5.5f, Time.time);
            float noiseZ = Mathf.PerlinNoise(10.5f, Time.time);

            Vector3 noiseDir = new Vector3(noiseX, noiseY, noiseZ);
            noiseDir = noiseDir.normalized * noiseForceStrength;

            body.velocity = flyDir + noiseDir;
        }
    }

    void Update()
    {
        if(!isActive && !body.isKinematic && body.velocity.magnitude < 0.001f)
        {
            body.isKinematic = true;
        }

        if(!canAttack)
        {
            damageCooldownTimer += Time.deltaTime;
            if(damageCooldownTimer > damageCooldown)
            {
                damageCooldownTimer = 0f;
                canAttack = true;
            }
        }
        
    }

    public void OnDeath()
    {
        //Decrease radius so it clips into the ground after falling
        GetComponent<SphereCollider>().radius *= 0.5f;

        body.useGravity = true;
        
        body.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        isActive = false;
    }

}
