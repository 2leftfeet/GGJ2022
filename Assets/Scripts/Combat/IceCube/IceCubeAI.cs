using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StateAI { idle, targeting, attacking, waiting }

public class IceCubeAI : MonoBehaviour
{
    Rigidbody rigidbody;
    StateAI currentState = StateAI.idle;
    [SerializeField]
    Transform Sigil;
    SigilControler sigilController;
    [SerializeField] float targetingDelay = 3f;
    [SerializeField] float looseDelay = 2f;
    [SerializeField] float attackLength = 2f;
    [SerializeField] float rotationSpeed = 0.001f;
    bool didNotLosePlayer = false;


    //timers
    float targetingStart;
    float looseStart;
    float attackStart;

    [SerializeField] float speed = 70f;
    bool rotateCube = false;
    bool CubeLaunched = false;

    [SerializeField] LayerMask wallLayer;

    [SerializeField]
    public Transform PlayerLocation;

    RaycastHit hit;

    private void Start()
    {

        rigidbody = GetComponent<Rigidbody>();
        sigilController = Sigil.GetComponent<SigilControler>();
    }


    private void FixedUpdate()
    {
        sigilController.SyncPostition(transform.position);

        if (currentState == StateAI.attacking && !CubeLaunched)
        {
            attackStart = Time.time;

            CubeLaunched = true;
            Vector3 velocity = rigidbody.velocity;
            velocity = transform.forward * speed;

            rigidbody.velocity = velocity;
        }
        if (currentState == StateAI.attacking && CubeLaunched)
        {
            if(Time.time - attackStart > attackLength)
            {
                CubeLaunched = false;
                currentState = StateAI.idle;
                sigilController.ChangeState(StateAI.idle);
            }
        }

        if (rotateCube)
        {
            Vector3 Cubedirection = PlayerLocation.transform.position - sigilController.transform.position;
            Cubedirection.y = 0;
            Quaternion toCubeRotation = Quaternion.LookRotation(Cubedirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, toCubeRotation, rotationSpeed * Time.time);
        }
    }

    private void Update()
    {
        if (Physics.Raycast(this.transform.position, PlayerLocation.position - this.transform.position, out hit))
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
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.layer);
        if (wallLayer == (wallLayer | (1 << other.gameObject.layer)))
        {
            CubeLaunched = false;
            currentState = StateAI.idle;
            sigilController.ChangeState(StateAI.idle);
        }
    }
}
