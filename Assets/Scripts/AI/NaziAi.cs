using UnityEngine;
using UnityEngine.AI;

public class NaziAi : MonoBehaviour
{
    // ��������� ����� ����������
    public Vector3 goal;
    private NavMeshAgent agent;
    void Start()
    {
        // ��������� ���������� ������
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
                Debug.Log("������� ���������");
                agent.destination = hit.position; // ���������� hit ��� ��������� �������
            }
            else
            {
                Debug.Log("���� �� �� NavMesh");
            }
        }
        else
        {
            Debug.Log("Agent �� ������");
        }
    }
}