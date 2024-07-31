using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSound : MonoBehaviour
{
    AudioSource audioSource;
    public float minPitch = 0.05f;
    private float pitchFromCar;
    private CarController cc;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = minPitch;
        cc = GetComponent<CarController>();
    }

    // Update is called once per frame
    void Update()
    {
        pitchFromCar = cc.carCurrentSpeed;
        if (pitchFromCar < minPitch)
            audioSource.pitch = minPitch;
        else
            audioSource.pitch = pitchFromCar;
    }
}