using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEdible : MonoBehaviour, IInteractable
{
    [SerializeField] Color glowColor = Color.black;
    [SerializeField] int healAmount = 10;

    bool isHovering = false;
    Renderer m_renderer;
   

    void Start()
    {
        m_renderer = GetComponent<Renderer>();
        m_renderer.material.EnableKeyword("_EMISSION");
    }

    void Update()
    {
        if(isHovering)
        {
            GameEvents.current.ConsumeUIEnterEvent();
            m_renderer.material.SetColor("_EmissionColor", glowColor);
        }
        else
        {
            GameEvents.current.ConsumeUIExitEvent();
            m_renderer.material.SetColor("_EmissionColor", Color.black);
        }

        isHovering = false;
    }

    public void Hover()
    {
        isHovering = true;
    }

    public void Interact(Transform interactee)
    {
        var health = interactee.GetComponent<Health>();
        if(health)
        {
            health.AddHealth(healAmount);
            Destroy(gameObject);
            //TODO: Spawn gibs
        }
    }
}
