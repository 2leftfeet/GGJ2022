using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnGibs : MonoBehaviour
{
    [SerializeField] float multiplyAmount = 4f;
    [SerializeField] GameObject[] gibs;
    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        for (int i = 0; i < 3; i++)
        {
            var randomIndex = Random.Range(0, 4);
            var gib = Instantiate(gibs[randomIndex],new Vector3(transform.position.x,transform.position.y,transform.position.z), Quaternion.identity);
            SetVelocity(gib.GetComponent<Rigidbody>());
        }
    }

    void SetVelocity(Rigidbody rb)
    {
        Vector3 velocityDir = Random.onUnitSphere;
        velocityDir.y = Mathf.Abs(velocityDir.y);

        velocityDir.y += multiplyAmount;
        rb.velocity = new Vector3(velocityDir.x,2f + velocityDir.y,velocityDir.z);
    }
}
