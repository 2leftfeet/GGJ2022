using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectileThatIsNotKnife : MonoBehaviour
{
    [SerializeField] float attackSpeed = 1f;    //shoots every x seconds
    [SerializeField] Transform projectile;     //waht is being shot
    [SerializeField] float projectileSpeed = 100f;//how fast are projectiles moving
    [SerializeField] int damage = 15;           //how much Damage are projectiles doing
    [SerializeField] Transform spawnLocation;   // where to spawn this if not selected automaticlly takes this.transform
    [SerializeField] public Transform target;          // a target that we are hunting
    [SerializeField] bool spawnProjectileEarly = false; // do we hover projectile before shooting 
    [SerializeField] DamageShareHealth AuthorsHealth; // do we hover projectile before shooting 
    [SerializeField] float chargeUpTime = 1f;

    float chargeupTimer = 0f;

    private bool readyToShoot = true;          // do we seee anemy and have a projectile
    private float releadTimer = 0f;

    public bool StopShooting = false; // for overrride from outside

    Transform latestProjectile;
     
    RaycastHit hit;

    bool ProjectileSpawned = false;

    void Start()
    {
        if (!spawnLocation)
            spawnLocation = transform;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        target = players[0].transform;
    }

    // Update is called once per frame
    void Update()
    {
        Reload();
        if (!StopShooting)
        {
            if (readyToShoot)
            {
                if (spawnProjectileEarly && !ProjectileSpawned)
                {
                    Spawn();
                };

                
            }
        }
        else
        {
            if(ProjectileSpawned)
            {
                Destroy(latestProjectile.gameObject);
            }
        }

        if(ProjectileSpawned)
        {
            chargeupTimer += Time.deltaTime;
            latestProjectile.transform.LookAt(target.position);
        }

        if (chargeupTimer > chargeUpTime && Physics.Raycast(spawnLocation.position, target.position - spawnLocation.position, out hit))
        {
            if (hit.transform.GetComponent<PlayerInput>())
            {
                chargeupTimer = 0f;
                Shoot(latestProjectile);
                // if (!spawnProjectileEarly && !ProjectileSpawned)
                //     Spawn();
                // //Shoot();
            }
        }
    }

    public void Reload()
    {
        if(Time.time - releadTimer > attackSpeed)
        {
            readyToShoot = true;
        }
    }



    public void Spawn()
    {
        spawnLocation.transform.LookAt(target);
        latestProjectile = Instantiate( projectile, spawnLocation.position, spawnLocation.rotation);
        latestProjectile.GetComponent<Projectile>().author = AuthorsHealth;
        latestProjectile.GetComponent<Projectile>().projectileDamage = damage;
        ProjectileSpawned = true;
        //latestProjectile.parent = transform;
        //Shoot(newprojectile);
    }

    public void Shoot(Transform projectile)
    {
        projectile.parent = null;
        releadTimer = Time.time;
        ProjectileSpawned = false;
        readyToShoot = false;
        Rigidbody rigidbody = projectile.GetComponent<Rigidbody>();
        Vector3 velocity = rigidbody.velocity;
        velocity = projectile.forward * projectileSpeed;
        rigidbody.velocity = velocity;
    }
}
