using UnityEngine;

public class GlobalUpdater : MonoBehaviour
{
    private void Update()
    {
        for (int i = 0; i < MonoCache.allObjects.Count; i++)
        {
            MonoCache.allObjects[i].Tick();
        }
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < MonoCache.allObjects.Count; i++)
        {
            MonoCache.allObjects[i].FixedTick();
        }
    }

    private void LateUpdate()
    {
        for (int i = 0; i < MonoCache.allObjects.Count; i++)
        {
            MonoCache.allObjects[i].LateTick();
        }
    }
}
