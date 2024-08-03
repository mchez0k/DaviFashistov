using UnityEngine;

public class NaziAi : MonoBehaviour
{
    // Положение точки назначения
    public Vector3 goal;
    private UnityEngine.AI.NavMeshAgent agent;
    void Start()
    {
        // Получение компонента агента
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if (goal != null)
        {
            Debug.Log("Пидорас обнаружен");
            agent.destination = goal;
        } else
        {
            Debug.Log("Страдаю хуйнёй");
        }
    }
}