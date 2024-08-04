using UnityEngine;
using UnityEngine.AI;

public class NaziAi : MonoBehaviour
{
    // Положение точки назначения
    public Vector3 goal;
    private NavMeshAgent agent;
    void Start()
    {
        // Получение компонента агента
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
    }

    private void FixedUpdate()
    {
        if (agent != null)
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(goal, out hit, 1.0f, NavMesh.AllAreas))
            {
                Debug.Log("Пидорас обнаружен");
                agent.destination = hit.position; // Используем hit для установки позиции
            }
            else
            {
                Debug.Log("Цель не на NavMesh");
            }
        }
        else
        {
            Debug.Log("Agent не найден");
        }
    }
}