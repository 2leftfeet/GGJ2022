using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SigilControler : MonoBehaviour
{
    StateAI currentState;
    float rotationSpeed = 1f;
    float bobingMultiplier = 0.005f;
    Vector3 clockwiseRotation;
    static Vector3 correctFaceDirectionCorrection = new Vector3(90f, 0f, 0f);
    private Vector3 playerPosition;

    [SerializeField]
    public GameObject _DEBUG_PLAYER_LOCATION;

    public Vector3 PlayerPosition { get => playerPosition; set => playerPosition = value; }
    public StateAI CurrentState { get => currentState; set => currentState = value; }

    // Start is called before the first frame update
    void Start()
    {
        clockwiseRotation = new Vector3(0f, 0f, rotationSpeed);
        CurrentState = StateAI.targeting;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (CurrentState)
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

    void Idling()
    {
        this.transform.Rotate(clockwiseRotation);
        //boobing
        this.transform.Translate((Vector3.forward * Mathf.Cos(Time.time))* bobingMultiplier);
    }

    void Targeting()
    {
        // change mat to red
        this.transform.LookAt(_DEBUG_PLAYER_LOCATION.transform);
        this.transform.Rotate(correctFaceDirectionCorrection);
        float amountTochange = Mathf.Pow(Mathf.Sin(Time.time * 10f), 3f) * 0.01f;
        Vector3 changeVector = new Vector3(amountTochange, amountTochange, amountTochange);
        this.transform.localScale += changeVector;
    }

    void Attacking()
    {
        float amountTochange =  Mathf.Pow(Mathf.Sin(Time.time * 20f), 3f) * 0.01f;
        Vector3 changeVector = new Vector3(amountTochange, amountTochange, amountTochange);
    }
    void Waiting()
    {

    }

}
