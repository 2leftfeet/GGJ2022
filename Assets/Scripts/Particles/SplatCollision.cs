using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class SplatCollision : MonoBehaviour
{
    ParticleSystem m_ParentParticle;
    ParticleDecalPool decalData;

    List<ParticleCollisionEvent> m_CollisionEvents;

    void Start()
    {
        m_ParentParticle = GetComponent<ParticleSystem>();
        m_CollisionEvents = new List<ParticleCollisionEvent>();
        decalData = FindObjectOfType<ParticleDecalPool>();
        
    }

    void OnParticleCollision(GameObject other)
    {
        ParticlePhysicsExtensions.GetCollisionEvents(m_ParentParticle, other, m_CollisionEvents);

        for (int i = 0; i < m_CollisionEvents.Count; i++)
        {
            //Debug.Log("Collided " + m_CollisionEvents.Count);
            if(decalData) decalData.ParticleHit(m_CollisionEvents[i]);
        }
    }
}
