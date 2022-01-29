using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class ParticleDecalPool : MonoBehaviour
{
    public int maxDecals = 100;

    ParticleSystem m_DecalParticles;
    int m_ParticleDecalIndex;
    ParticleData[] m_ParticleData;
    ParticleSystem.Particle[] m_Particles;

    void Start()
    {
        m_DecalParticles = GetComponent<ParticleSystem>();
        m_Particles = new ParticleSystem.Particle[maxDecals];
        m_ParticleData = new ParticleData[maxDecals];
        for (int i = 0; i < maxDecals; i++)
        {
            m_ParticleData[i] = new ParticleData();
        }
    }

    public void ParticleHit(ParticleCollisionEvent particleCollisionEvent)
    {
        SetParticleData(particleCollisionEvent);
        DisplayParticles();
    }

    void DisplayParticles()
    {
        for (int i = 0; i < m_ParticleData.Length; i++)
        {
            m_Particles[i].position = m_ParticleData[i].position;
            m_Particles[i].rotation3D = m_ParticleData[i].rotation;
        }
        
        m_DecalParticles.SetParticles(m_Particles, m_Particles.Length);
    }

    void SetParticleData(ParticleCollisionEvent particleCollisionEvent)
    {
        if (m_ParticleDecalIndex >= maxDecals)
        {
            m_ParticleDecalIndex = 0;
        }

        m_ParticleData[m_ParticleDecalIndex].position = particleCollisionEvent.intersection;
        Vector3 particleRotationEuler = Quaternion.LookRotation(particleCollisionEvent.normal).eulerAngles;
        particleRotationEuler.z = Random.Range(0, 360);
        m_ParticleData[m_ParticleDecalIndex].rotation = particleRotationEuler;

        m_ParticleDecalIndex++;
    }
}
