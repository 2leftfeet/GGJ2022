using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerKick : MonoBehaviour
{
    public Animator animator;
    public float kickCooldown = 0.5f;
    public GameObject gibsVFX;
    
    float kickTimer = 0f;
    bool canKick = true;
    SphereCollider myCol;

    void Start()
    {
        myCol = GetComponent<SphereCollider>();
    }


    // Update is called once per frame
    void Update()
    {
        if(canKick && Input.GetKey(KeyCode.Q))
        {
            animator.SetTrigger("DoKick");
            canKick = false;
            var colliders = Physics.OverlapSphere(transform.position + myCol.center, myCol.radius);
            for(int i = 0; i < colliders.Length; ++i)
            {
                var shareDmgHealth = colliders[i].GetComponent<DamageShareHealth>();

                if(shareDmgHealth && shareDmgHealth.vulnerability == VulnerableTo.All)
                {
                    Instantiate(gibsVFX, colliders[i].transform.position, Quaternion.identity);
                    Destroy(shareDmgHealth.gameObject);

                }
            }
        }

        if(!canKick)
        {
            kickTimer += Time.deltaTime;
            if(kickTimer > kickCooldown)
            {
                kickTimer = 0f;
                canKick = true;
            }
        }
    }
}
