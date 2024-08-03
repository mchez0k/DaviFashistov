using UnityEngine;

public class NaziAi : MonoBehaviour
{
    // ��������� ����� ����������
    public Vector3 goal;
    private UnityEngine.AI.NavMeshAgent agent;
    void Start()
    {
        // ��������� ���������� ������
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        if (goal != null)
        {
            Debug.Log("������� ���������");
            agent.destination = goal;
        } else
        {
            Debug.Log("������� �����");
        }
    }
}