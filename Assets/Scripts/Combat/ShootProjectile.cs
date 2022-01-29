using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    
    PlayerInput playerInput;
    Health playerHealth;

    float reloadTimer = 0f;
    bool canShoot = true;

    [SerializeField] float reloadTime = 0.3f;
    [SerializeField] DaggerProjectile projectile;
    [SerializeField] Transform spawnTransform;
    [SerializeField] Transform daggerEquipped;
    [SerializeField] float daggerLoweredByAmount = 0.6f;
    [SerializeField] Transform propDaggerPrefab;
    [SerializeField] int maxPropDaggerCount;

    Queue<Transform> propDaggers;

    float startingKnifeY;
    float loweredKnifeY;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        playerHealth = GetComponent<PlayerHealth>();

        startingKnifeY = daggerEquipped.localPosition.y;
        loweredKnifeY = startingKnifeY - daggerLoweredByAmount;

        propDaggers = new Queue<Transform>();
    }

    void Update()
    {
        if(!canShoot)
        {
            reloadTimer += Time.deltaTime;
            if(reloadTimer > reloadTime)
            {
                reloadTimer = reloadTime;
                canShoot = true;
            }

            Vector3 knifePosition = daggerEquipped.localPosition;
            knifePosition.y = Mathf.Lerp(loweredKnifeY, startingKnifeY, reloadTimer/reloadTime);

            daggerEquipped.localPosition = knifePosition;
        }
    }

    void TryShoot()
    {
        if(canShoot)
        {
            reloadTimer = 0f;
            canShoot = false;

            DaggerProjectile newProj = Instantiate(projectile, spawnTransform.position, spawnTransform.rotation);
            newProj.author = playerHealth;
            newProj.launchPos = spawnTransform.position;
        }
    }

    void OnEnable()
    {
        playerInput.HoldPrimaryAttack += TryShoot;
    }

    void OnDisable()
    {
        playerInput.HoldPrimaryAttack -= TryShoot;
    }

    public void SpawnPropKnifeAtCollision(Vector3 position, Vector3 launchPosition){
        if(propDaggers.Count < maxPropDaggerCount)
        {
            Transform newDagger = Instantiate(propDaggerPrefab, position, Quaternion.LookRotation(launchPosition - position));
            propDaggers.Enqueue(newDagger);
        }
        else
        {
            Transform oldDagger = propDaggers.Dequeue();
            oldDagger.position = position;
            oldDagger.rotation = Quaternion.LookRotation(launchPosition - position);
            propDaggers.Enqueue(oldDagger);
        }
    }


}
