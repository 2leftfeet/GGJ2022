using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAI : MonoBehaviour
{
    ShootProjectileThatIsNotKnife shootyPart;
    [SerializeField] float minShootingdistance = 30f;
    [SerializeField] float eyeLookSpeed = 20f;
    RaycastHit hit;
    Transform player;
    // blender thingy
    static Vector3 correctFaceDirectionCorrection = new Vector3(90f, 0f, 0f);
    // Start is called before the first frame update
    void Start()
    {
        shootyPart = GetComponent<ShootProjectileThatIsNotKnife>();
        player = shootyPart.target;
    }

    // Update is called once per frame
    void Update()
    {
        if(!player)
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
