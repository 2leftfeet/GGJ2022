using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawbladeSpinner : MonoBehaviour
{
    public float speed = 50f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(transform.right, speed * Time.deltaTime, Space.World);
    }
}
