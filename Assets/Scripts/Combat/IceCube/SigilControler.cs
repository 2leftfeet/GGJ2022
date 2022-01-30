using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SigilControler : MonoBehaviour
{
    StateAI currentState;
    float rotationSpeed = 1f;
    float sigilRotationSpeed = 20f;
    float bobingMultiplier = 0.005f;
    Vector3 clockwiseRotation;
    static Vector3 correctFaceDirectionCorrection = new Vector3(90f, 0f, 0f);
    public bool isAlive = true;

    private Transform playerPosition;
    public Transform PlayerPosition { get => playerPosition; set => playerPosition = value; }
    public StateAI CurrentState { get => currentState; set => currentState = value; }

    // Start is called before the first frame update
    void Start()
    {
        clockwiseRotation = new Vector3(0f, 0f, rotationSpeed);
        currentState = StateAI.idle;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isAlive)
        {
            switch (currentState)
            {
                case StateAI.idle:
                    Idling();
                    break;
                case StateAI.targeting:
                    Targeting();
                    break;
                case StateAI.attacking:
                    Attacking();
                    break;
                case StateAI.waiting:
                    Waiting();
                    break;
                default:
                    break;
            }
        }
    }

    void Idling()
    {
        transform.Rotate(clockwiseRotation);
        //boobing
        transform.Translate((Vector3.forward * Mathf.Cos(Time.time))* bobingMultiplier);
    }

    void Targeting()
    {
        // change mat to red
        if (playerPosition)
        {
            Vector3 direction = playerPosition.transform.position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(correctFaceDirectionCorrection);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, sigilRotationSpeed * Time.fixedDeltaTime);
        }
        ChangeScaleSin(20f, 0.01f);
    }

    void Attacking()
    {
        ChangeScaleSin(10f, 0.01f);
    }
    void Waiting()
    {
        ChangeScaleSin(5f, 0.01f);
    }


    private void ChangeScaleSin(float freq, float amount)
    {
        float amountTochange = Mathf.Pow(Mathf.Sin(Time.time * freq), 3f) * amount;
        Vector3 changeVector = new Vector3(amountTochange, amountTochange, amountTochange);
        transform.localScale += changeVector;
    }

    public void ChangeState(StateAI state)
    {
        CurrentState = state;
        transform.localScale = Vector3.one;
    }

    public void SyncPostition(Vector3 pos)
    {
        transform.position = new Vector3(pos.x, pos.y, pos.z);
    }

}
