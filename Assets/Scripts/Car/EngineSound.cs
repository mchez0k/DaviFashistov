using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSound : MonoCache
{
    AudioSource audioSource;
    public float minPitch = 0.2f;
    internal float engineModifier;
    internal CarController carController;

    public float baseDetectionRadius = 100f; // Радиус, в котором враги могут слышать звук
    public LayerMask naziLayer; // Слой для определения игроков

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = minPitch;
        carController = GetComponent<CarController>();
    }

    public override void OnFixedTick()
    {
        PlayEngineSound();
    }

    private void PlayEngineSound()
    {
        engineModifier = carController.carCurrentSpeed;
        if (engineModifier < minPitch)
            audioSource.pitch = minPitch;
        else
            audioSource.pitch = engineModifier;
    }
}