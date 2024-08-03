using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaziAi : MonoBehaviour
{
    // Положение точки назначения
    public Transform goal;
    private UnityEngine.AI.NavMeshAgent agent;
    void Start()
    {
        // Получение компонента агента
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        agent.destination = goal.position;
    }
}
