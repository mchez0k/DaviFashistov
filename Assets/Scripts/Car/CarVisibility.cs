using System.Collections.Generic;
using System;
using UnityEngine;

public class CarVisibility : MonoCache
{
    [SerializeField] private float baseDetectionRadius = 100f;
    [SerializeField] private LayerMask naziLayer;
    [SerializeField] private LayerMask playerMask;

    private bool isDetected;

    private EngineSound engineSound;

    private void Awake()
    {
        engineSound = GetComponent<EngineSound>();
    }

    public override void OnFixedTick()
    {
        CheckForAIVisible();
    }

    public void CheckForAIVisible()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, baseDetectionRadius, naziLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (!hitCollider.transform.root.TryGetComponent(out NaziAi naziBot)) continue;
            Vector3 dir = transform.position - naziBot.transform.position;
            float angle = Vector3.Angle(naziBot.transform.forward, dir);
            if (angle < naziBot.detectionAngle / 2)
            {
                Debug.Log("Попал в поле зрения");
                float distanceToTarget = Vector3.Distance(naziBot.transform.position, transform.position);
                Debug.DrawRay(naziBot.transform.position, dir.normalized * distanceToTarget, Color.red);
                if (!Physics.Raycast(naziBot.transform.position, dir, distanceToTarget, playerMask))
                {
                    Debug.Log("Увидел");
                    isDetected = true;
                    naziBot.VisibleDetected(transform.position);
                }
            }
            if (isDetected) continue;
            if (dir.magnitude < baseDetectionRadius * engineSound.engineModifier)
            {
                naziBot.SoundDetected(transform.position);
            }
        }
    }
}