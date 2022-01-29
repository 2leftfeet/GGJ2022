using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyEdible : MonoBehaviour, IInteractable
{
    [SerializeField] Color glowColor = Color.black;

    bool isHovering = false;
    Renderer m_renderer;
   

    void Start()
    {
        m_renderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if(isHovering)
        {
            m_renderer.material.SetColor("_EmissionColor", glowColor);
        }
        else
        {
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

    }
}
