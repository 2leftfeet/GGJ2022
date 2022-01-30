using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour, IDeadable
{
    ShootProjectileThatIsNotKnife shootyPart;
    [SerializeField] float minShootingdistance = 30f;
    [SerializeField] float eyeLookSpeed = 20f;
    bool isAlive = true;
    RaycastHit hit;
    Transform player;
    Rigidbody rigidbody;
    EnemyEdible edible;
    // blender thingy
    static Vector3 correctFaceDirectionCorrection = new Vector3(0f, 90f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        edible = GetComponent<EnemyEdible>();
        rigidbody = GetComponent<Rigidbody>();
        shootyPart = GetComponent<ShootProjectileThatIsNotKnife>();
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        player = players[0].transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (isAlive) {
            if (!player)
                player = shootyPart.target;

            Vector3 direction = player.position - transform.position;
            Quaternion toRotation = Quaternion.LookRotation(direction) * Quaternion.Euler(correctFaceDirectionCorrection);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, eyeLookSpeed * Time.fixedDeltaTime);

            if (Vector3.Distance(this.transform.position, player.position) < minShootingdistance)
            {
                if (Physics.Raycast(this.transform.position, player.position - this.transform.position, out hit))
                {
                    if (hit.transform.GetComponent<PlayerInput>())
                    {
                        shootyPart.StopShooting = false;
                    }
                }
            }
            else
            {
                shootyPart.StopShooting = true;
            }
        }
    }
    public void OnDeath()
    {
        transform.tag = "Untagged";
        isAlive = false;
        shootyPart.StopShooting = true;
        //Decrease radius so it clips into the ground after falling
        Destroy(GetComponent<Health>());
        Debug.Log("eyeTurret die, RIP");
        edible.enabled = true;
        rigidbody.isKinematic = false;
        rigidbody.useGravity = true;
        rigidbody.AddForce(Vector3.forward);
    }
}
