using System.Collections.Generic;
using UnityEngine;

public class MonoCache : MonoBehaviour
{
    public static List<MonoCache> allObjects = new List<MonoCache>(150);

    private void OnEnable() => allObjects.Add(this);
    private void OnDisable() => allObjects.Remove(this);
    private void OnDestroy() => allObjects.Remove(this);

    public void Tick() => OnTick();

    public void FixedTick() => OnFixedTick();

    public void LateTick() => OnLateTick();

    public virtual void OnTick() { }

    public virtual void OnFixedTick() { }

    public virtual void OnLateTick() { }
}
