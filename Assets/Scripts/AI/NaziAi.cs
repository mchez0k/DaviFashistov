using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

public class NaziAi : MonoBehaviour
{
    private NavMeshAgent agent;
    void Awake()
    {
        // Получение компонента агента
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
    }

    internal void SetDestination(Vector3 pos)
    {
        agent.destination = pos;
    }
}