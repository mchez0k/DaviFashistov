using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSound : MonoBehaviour
{
    AudioSource audioSource;
    public float minPitch = 0.2f;
    private float engineModifier;
    private CarController cc;
    private CarVisibility cvis;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = minPitch;
        cc = GetComponent<CarController>();
        cvis = GetComponent<CarVisibility>();
    }

    public void CheckForAISound()
    {
        engineModifier = cc.carCurrentSpeed;
        if (engineModifier < minPitch)
            audioSource.pitch = minPitch;
        else
            audioSource.pitch = engineModifier;

        cvis.hearingRangeMod = engineModifier;
    }
}