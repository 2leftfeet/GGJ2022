using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using Random = UnityEngine.Random;

public class SpawnGibs : MonoBehaviour
{
    [SerializeField] float multiplyAmount = 4f;
    [SerializeField] GameObject[] gibs;
    bool m_Deathing = false;
    void Start()
    {
        Spawn();
    }

    void Spawn()
    {
        for (int i = 0; i < 3; i++)
        {
            var randomIndex = Random.Range(0, 4);
            var gib = Instantiate(gibs[randomIndex],new Vector3(transform.position.x,transform.position.y,transform.position.z), Quaternion.identity, transform);
            SetVelocity(gib.GetComponent<Rigidbody>());
        }

        StartCoroutine(Destroy());
    }

    void SetVelocity(Rigidbody rb)
    {
        Vector3 velocityDir = Random.onUnitSphere;
        velocityDir.y = Mathf.Abs(velocityDir.y);

        velocityDir.y += multiplyAmount;
        rb.velocity = new Vector3(velocityDir.x,2f + velocityDir.y,velocityDir.z);
    }

    void Update()
    {
        if (m_Deathing) transform.position = new Vector3(transform.position.x, transform.position.y - 0.5f * Time.deltaTime, transform.position.z);
    }

    private IEnumerator Destroy()
    {
        yield return new WaitForSeconds(6);
        foreach (Transform child in transform)
        {
            if (child.GetComponent<BoxCollider>())
            {
                child.GetComponent<BoxCollider>().enabled = false;
                child.GetComponent<Rigidbody>().isKinematic = true;
            }
        }

        m_Deathing = true;
        yield return new WaitForSeconds(3);
        Destroy(this.gameObject);
    }
}
