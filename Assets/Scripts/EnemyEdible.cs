using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEdible : MonoBehaviour, IInteractable
{
    [SerializeField] Color glowColor = Color.black;
    [SerializeField] int healAmount = 10;
    [SerializeField] Renderer m_renderer;
    [SerializeField] GameObject gibsVFX;

    bool isHovering = false;
   

    void Start()
    {
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

            Destroy(Instantiate(gibsVFX, transform.position, Quaternion.identity), 10f);
            
        }
    }
}
