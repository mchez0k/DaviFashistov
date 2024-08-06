using UnityEngine;
using UnityEngine.AI;

public class CarVisibility : MonoCache
{
    [SerializeField] private float baseDetectionRadius = 100f;
    [SerializeField] private LayerMask naziLayer;
    private CarController cc;
    private EngineSound engineSound;

    void Awake()
    {
        cc = GetComponent<CarController>();
        engineSound = GetComponent<EngineSound>();
    }

    public void CheckForAIVisible()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, baseDetectionRadius, naziLayer);
        foreach (var hitCollider in hitColliders)
        {
            hitCollider.transform.root.TryGetComponent(out NaziAi naziBot);
            naziBot.TryToDetect(transform.position, cc.GetSpeedVector(), baseDetectionRadius*engineSound.engineModifier);            
        }
    }
}
