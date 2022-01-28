using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour
{
    
    PlayerInput playerInput;

    float reloadTimer = 0f;
    bool canShoot = true;

    [SerializeField] float reloadTime = 0.3f;
    [SerializeField] DaggerProjectile projectile;
    [SerializeField] Transform spawnTransform;
    [SerializeField] Transform knifeEquipped;
    [SerializeField] float knifeLoweredByAmount = 0.6f;

    float startingKnifeY;
    float loweredKnifeY;

    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        startingKnifeY = knifeEquipped.localPosition.y;
        loweredKnifeY = startingKnifeY - knifeLoweredByAmount;
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

            Vector3 knifePosition = knifeEquipped.localPosition;
            knifePosition.y = Mathf.Lerp(loweredKnifeY, startingKnifeY, reloadTimer/reloadTime);

            knifeEquipped.localPosition = knifePosition;
        }
    }

    void TryShoot()
    {
        if(canShoot)
        {
            reloadTimer = 0f;
            canShoot = false;

            Instantiate(projectile, spawnTransform.position, spawnTransform.rotation);
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

}
