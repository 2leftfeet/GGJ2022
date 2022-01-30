using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StateAI { idle, targeting, attacking, waiting }

public class IceCubeAI : MonoBehaviour, IDeadable
{
    Rigidbody rigidbody;

    StateAI currentState = StateAI.idle;
    [SerializeField]
    Transform Sigil;
    SigilControler sigilController;
    [SerializeField] float targetingDelay = 3f;
    [SerializeField] float looseDelay = 2f;
    [SerializeField] float attackLength = 2f;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float healthDeformationSpeed = 0.75f;
    [SerializeField] float agroRange = 30f;
    bool didNotLosePlayer = false;
    bool isAlive = true;
    MeshRenderer meshRenderer;

    private Health myHealth;

    [SerializeField] Rigidbody SigilRigidbody;
    [SerializeField] MeshCollider SigilCollider;
    [SerializeField] EnemyEdible SigilEdible;
    [SerializeField] GameObject gibsVFX;

    //timers
    float targetingStart;
    float looseStart;
    float attackStart;

    [SerializeField] float speed = 70f;
    bool rotateCube = false;
    bool CubeLaunched = false;

    DamageShareHealth damageShareHealth;

    [SerializeField] LayerMask wallLayer;

    public Transform PlayerLocation;

    enum attacktype {slide, shoot};
    [SerializeField] attacktype[] attackOrder = {attacktype.slide, attacktype.shoot, attacktype.shoot};

    attacktype nextAttack;
    int nextAttackCounter = 0;

    [SerializeField]
    ShootProjectileThatIsNotKnife[] shooters;

    bool lowHP = false;
    RaycastHit hit;
    int StartingHealth;
    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        damageShareHealth = GetComponent<DamageShareHealth>();
        myHealth = GetComponent<Health>();
        rigidbody = GetComponent<Rigidbody>();
        sigilController = Sigil.GetComponent<SigilControler>();
        StartingHealth = damageShareHealth.healthAmount;

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        PlayerLocation = players[0].transform;


    }


    private void FixedUpdate()
    {
        if (isAlive)
        {
            ManageHealth(myHealth.healthAmount, StartingHealth);

            sigilController.SyncPostition(transform.position);

            if (currentState == StateAI.attacking && !CubeLaunched)
            {
                
                attackStart = Time.time;


                CubeLaunched = true;
                if (nextAttack == attacktype.slide)
                {
                    Vector3 velocity = rigidbody.velocity;
                    velocity = transform.forward * speed;
                    rigidbody.velocity = velocity;
                }
                if (nextAttack == attacktype.shoot)
                {
                    foreach (ShootProjectileThatIsNotKnife e in shooters)
                    {
                        e.StopShooting = false;
                    }
                }
                
            }
            if (currentState == StateAI.attacking && CubeLaunched)
            {
                if (Time.time - attackStart > attackLength)
                {
                    CubeLaunched = false;
                    foreach (ShootProjectileThatIsNotKnife e in shooters)
                    {
                        e.StopShooting = true;
                    }
                    currentState = StateAI.idle;
                    sigilController.ChangeState(StateAI.idle);
                    nextAttack = ChangeNextAttack();
                }
            }

            if (rotateCube)
            {
                Vector3 Cubedirection = PlayerLocation.transform.position - sigilController.transform.position;
                Cubedirection.y = 0;
                Quaternion toCubeRotation = Quaternion.LookRotation(Cubedirection);
                transform.rotation = Quaternion.Lerp(transform.rotation, toCubeRotation, rotationSpeed);
            }

        }
    }

    private void Update()
    {
        if (isAlive)
        {
            if (Physics.Raycast(this.transform.position, PlayerLocation.position - this.transform.position, out hit, agroRange))
            {

                // if i see player
                if (hit.transform.GetComponent<PlayerInput>())
                {
                    if (currentState == StateAI.waiting)
                    {
                        targetingDelay += (Time.time - looseStart);
                        currentState = StateAI.targeting;
                        sigilController.ChangeState(StateAI.targeting);
                    }
                    if (currentState == StateAI.targeting)
                    {
                        sigilController.PlayerPosition = PlayerLocation;
                        if (Time.time - targetingStart > targetingDelay / 2)
                        {
                            rotateCube = true;
                        }
                        if (Time.time - targetingStart > targetingDelay)
                        {
                            rotateCube = false;
                            currentState = StateAI.attacking;
                            sigilController.ChangeState(StateAI.attacking);
                        }
                    }
                    if (currentState == StateAI.idle)
                    {
                        currentState = StateAI.targeting;
                        sigilController.ChangeState(StateAI.targeting);
                        targetingStart = Time.time;
                    }
                }
                else
                {
                    DidntHit();
                }
            }
            else
            {
                DidntHit();
            }

            void DidntHit()
            {
                if (currentState == StateAI.waiting)
                {
                    if (Time.time - looseStart > looseDelay)
                    {
                        currentState = StateAI.idle;
                        sigilController.ChangeState(StateAI.idle);
                    }
                }
                if (currentState == StateAI.targeting)
                {
                    looseStart = Time.time;
                    currentState = StateAI.waiting;
                    sigilController.ChangeState(StateAI.waiting);
                }
            }
        }
    }

    void ManageHealth(int currentHealth, int maxHealth)
    {
        
        float percentage = (float)currentHealth / (float)maxHealth; // procentage value

        if (percentage > 0.5f)
            this.transform.localScale = Vector3.one * (Mathf.Lerp(this.transform.localScale.x,percentage, healthDeformationSpeed * Time.deltaTime));

        if (percentage < 0.5f && percentage > 0.25f)
            this.transform.localScale = Vector3.one * (Mathf.Lerp(this.transform.localScale.x, percentage, healthDeformationSpeed * Time.deltaTime));

        if (percentage < 0.25f)
        {
            meshRenderer.enabled = false;
            rigidbody.isKinematic = true;
            rigidbody.useGravity = false;
            lowHP = true;


            damageShareHealth.vulnerability = VulnerableTo.All;
        }
        else if (!rigidbody.useGravity && percentage > 0.25f)
        {

            rigidbody.isKinematic = false;
            rigidbody.useGravity = true;
            meshRenderer.enabled = true;
            lowHP = false;
            this.transform.localScale = Vector3.one * 0.5f;
            damageShareHealth.vulnerability = VulnerableTo.Fire;
        }
    }

    public void OnDeath()
    {
        isAlive = false;
        sigilController.isAlive = false;
        transform.root.tag = "Untagged";
        BoxCollider[] colliders = GetComponents<BoxCollider>();
        foreach (BoxCollider box in colliders)
        {
            if (!box.isTrigger)
            {
                box.size = new Vector3(6f, 6f, 1f);
            }
        }
        
        //Decrease radius so it clips into the ground after falling
        Destroy(GetComponent<Health>());
        Debug.Log("IceCube die, RIP");

        SigilEdible.enabled = true;
        SigilCollider.enabled = true;
        SigilRigidbody.isKinematic = false;
        SigilRigidbody.useGravity = true;
        SigilRigidbody.AddForce(Vector3.forward);
        this.gameObject.SetActive(false);
    }

    attacktype ChangeNextAttack()
    {
        if (nextAttackCounter > attackOrder.Length - 1)
            nextAttackCounter = 0;
        var answer = attackOrder[nextAttackCounter];
        nextAttackCounter++;
        if (lowHP)
            return attacktype.shoot;
        return answer;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isAlive) {
            if (wallLayer == (wallLayer | (1 << other.gameObject.layer)))
            {
                CubeLaunched = false;
                nextAttack = ChangeNextAttack();
                currentState = StateAI.idle;
                sigilController.ChangeState(StateAI.idle);
            }

            var health = other.GetComponent<Health>();

            if (health && currentState == StateAI.attacking)
            {
                var player = other.GetComponent<PlayerInput>();
                if (player)
                {
                    health.ReduceHealth(myHealth.healthAmount);
                }
                else
                {
                    Destroy(Instantiate(gibsVFX, other.transform.position, Quaternion.identity), 10f);
                    Destroy(other.transform.root.gameObject);
                }
            }
        }
    }
}
