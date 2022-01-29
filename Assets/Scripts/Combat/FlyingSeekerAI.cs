using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingSeekerAI : MonoBehaviour, IDeadable
{
    Transform playerT;
    Rigidbody body;

    [SerializeField] float flySpeed = 5f;
    [SerializeField] float noiseForceStrength = 1f;

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
            Vector3 flyDir = playerT.position - transform.position;
            flyDir = flyDir.normalized;

            flyDir *= flySpeed;

            float noiseX = Mathf.PerlinNoise(0.5f, Time.time);
            float noiseY = Mathf.PerlinNoise(5.5f, Time.time);
            float noiseZ = Mathf.PerlinNoise(10.5f, Time.time);

            Vector3 noiseDir = new Vector3(noiseX, noiseY, noiseZ);
            noiseDir = noiseDir.normalized * noiseForceStrength;

            body.velocity = flyDir + noiseDir;
        }
        
    }

    public void OnDeath()
    {
        //Decrease radius so it clips into the ground after falling
        GetComponent<SphereCollider>().radius *= 0.5f;

        body.useGravity = true;
        isActive = false;
    }

}
