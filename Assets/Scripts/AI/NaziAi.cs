using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaziAi : MonoBehaviour
{
    // ��������� ����� ����������
    public Transform goal;
    private UnityEngine.AI.NavMeshAgent agent;
    void Start()
    {
        // ��������� ���������� ������
        agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }

    private void FixedUpdate()
    {
        agent.destination = goal.position;
    }
}
