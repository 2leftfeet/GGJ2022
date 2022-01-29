using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StateAI { idle, targeting, attacking, waiting }

[RequireComponent(typeof(SigilControler))]
public class IceCubeAI : MonoBehaviour
{
    StateAI currentState = StateAI.idle;
    SigilControler sigilControler;
    float targetingDelay = 0.5f;
    float findDelay = 0.5f;
    bool didNotLosePlayer = false;
    float targetingStart;


    [SerializeField]
    Transform PlayerLocation;

    RaycastHit hit;

    private void Start()
    {
        sigilControler = GetComponent<SigilControler>();
    }



    
    private void Update()
    {
        if(Physics.Raycast(this.transform.position, PlayerLocation.position, out hit))
        {
            if (hit.transform.GetComponent<PlayerInput>())
            {
                
                if(currentState == StateAI.targeting)
                {
                    if(Time.time - targetingStart > targetingDelay)
                    {
                        currentState = StateAI.attacking;
                        sigilControler.CurrentState = StateAI.attacking;
                    }
                }
                if(currentState == StateAI.idle)
                {
                    currentState = StateAI.targeting;
                    sigilControler.CurrentState = StateAI.targeting;
                    targetingStart = Time.time;
                }
            }
        }
        else
        {
            if (currentState == StateAI.targeting)
            {
                currentState = StateAI.idle;
                sigilControler.CurrentState = StateAI.idle;
            }
        }

    }



}
