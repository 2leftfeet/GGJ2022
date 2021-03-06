using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSeekerAI : MonoBehaviour, IDeadable
{
    Transform playerT;
    Rigidbody body;
    float damageCooldownTimer = 0f;
    float damageChargeupTimer = 0f;
    bool canAttack;

    [SerializeField] float flySpeed = 5f;
    [SerializeField] float noiseForceStrength = 1f;
    [SerializeField] float damageRange = 1f;
    [SerializeField] float damageCooldown = 2f;
    [SerializeField] float damageChargeup = 1f;
    [SerializeField] int damageAmount = 30;
    [SerializeField] float damagePushForce = 50f;
    [SerializeField] float damagePushForceUpwards = 20f;
    [SerializeField] float startFollowRange = 20f;

    [SerializeField] Material normalMat;
    [SerializeField] Material angryMat;

    bool isActive = true;
    bool hasStartedUp = false;
    float noiseOffset;
    EnemyEdible edible;
    Renderer render;


    void Start()
    {
        playerT = FindObjectOfType<PlayerMovement>().transform;
        body = GetComponent<Rigidbody>();
        edible = GetComponent<EnemyEdible>();
        render = GetComponentInChildren<Renderer>();
        edible.enabled = false;


        noiseOffset = Random.Range(0f, 10000f);
    }

    void FixedUpdate()
    {
        Vector3 dirToPlayer = playerT.position - transform.position;
        if(isActive && hasStartedUp)
        {
            Vector3 flyDir = dirToPlayer.normalized;

            flyDir = flyDir * flySpeed;

            if(canAttack && dirToPlayer.magnitude < damageRange)
            {
                render.material = angryMat;
                damageChargeupTimer += Time.deltaTime;
                if(damageChargeupTimer > damageChargeup)
                {
                    
                    canAttack = false;
                    playerT.GetComponent<Health>().ReduceHealth(damageAmount);
                    playerT.GetComponent<Rigidbody>().AddForce(dirToPlayer * damagePushForce);
                    playerT.GetComponent<Rigidbody>().AddForce(Vector3.up * damagePushForceUpwards);

                    damageChargeupTimer = 0f;    
                }
            }
            else
            {
                render.material = normalMat;
                damageChargeupTimer = 0f;
            }

            float noiseX = Mathf.PerlinNoise(0.5f, Time.time + noiseOffset);
            float noiseY = Mathf.PerlinNoise(5.5f, Time.time + noiseOffset);
            float noiseZ = Mathf.PerlinNoise(10.5f, Time.time + noiseOffset);

            Vector3 noiseDir = new Vector3(noiseX, noiseY, noiseZ);
            noiseDir = noiseDir.normalized * noiseForceStrength;

            body.velocity = flyDir + noiseDir;
            transform.LookAt(playerT.position, Vector3.up);

        }
        
        if(!hasStartedUp && dirToPlayer.magnitude < startFollowRange)
        {
            hasStartedUp = true;
        }
        
    }

    void Update()
    {
        if(!isActive && !body.isKinematic && body.IsSleeping() )
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
        transform.tag = "Untagged";
        //Decrease radius so it clips into the ground after falling
        //GetComponent<SphereCollider>().radius *= 0.5f;
        Destroy(GetComponent<Health>());
        body.useGravity = true;
        
        body.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ;
        isActive = false;

        edible.enabled = true;
    }

}
