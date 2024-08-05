using UnityEngine;

public class CarVisibility : MonoCache
{
    [SerializeField] private float baseDetectionRadius = 100f;
    [SerializeField] private LayerMask naziLayer;
    internal float hearingRangeMod = 0.5f;

    public void CheckForAIVisible()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, baseDetectionRadius, naziLayer);
        foreach (var hitCollider in hitColliders)
        {
            hitCollider.transform.root.TryGetComponent(out NaziAi naziBot);
            naziBot.TryToDetect(transform.position, baseDetectionRadius*hearingRangeMod);
        }
    }
}
