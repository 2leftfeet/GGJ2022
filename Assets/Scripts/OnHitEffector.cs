using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class OnHitEffector : MonoBehaviour
{
    public Volume volume;
    public Vignette vignette;

    public float vignetteSlider;

    void Start()
    {
        volume = FindObjectOfType<Volume>();
        volume.profile.TryGet(out vignette);
        GameEvents.current.onPlayerTakeDamageEvent += OnHit;
    }

    public void OnHit()
    {
        vignette.intensity.value = 0.5f;
    }

    void Update()
    {
        LowerValue();
    }

    public void LowerValue()
    {
        if (vignette.intensity.value > 0)
        {
            vignette.intensity.value -= 0.1f * Time.deltaTime;
        }
    }
}
