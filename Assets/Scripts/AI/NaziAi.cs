using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using ZovRodini;

public class NaziAi : MonoCache
{
    private NavMeshAgent agent;
    internal StateManager currentState;
    internal float commandDelay = 0.4f;
    private float currentDelay = 0;
    public float visibilityAngleMinCos = 0.5f;
    void Awake()
    {
        // Получение компонента агента
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
    }

    public override void OnFixedTick()
    {
        if (currentDelay > 0)
        {
            currentDelay -= Time.fixedDeltaTime;
        }
    }

    internal void TryToDetect(Vector3 pos, float hearingRange)
    {
        Vector3 dir = pos - transform.position;
        if (dir.normalized.x * transform.forward.x + dir.normalized.y * transform.forward.y + dir.normalized.z * transform.forward.z > visibilityAngleMinCos) { 
            VisibleDetected(pos);            
        }
        else if (dir.magnitude < hearingRange)
        {
            SoundDetected(pos);
        } 
        else
        {
            Debug.Log("Not detected");
        }
    }

    internal void SoundDetected(Vector3 pos)
    {
        if (currentDelay > 0) return;
        Debug.Log("Sound detected");
        //transform.forward = Vector3.Lerp(transform.forward, pos - transform.position, 0.1f);
        agent.destination = pos;
        currentDelay = commandDelay;
    }

    internal void VisibleDetected(Vector3 pos)
    {
        if (currentDelay > 0) return;
        Debug.Log("Visible detected");
        transform.forward = Vector3.Lerp(transform.forward, pos - transform.position, 0.1f);
        agent.destination = pos;
        currentDelay = commandDelay;
    }


}