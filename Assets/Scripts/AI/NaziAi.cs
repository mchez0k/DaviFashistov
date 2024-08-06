using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using ZovRodini;

public class NaziAi : MonoCache
{
    private NavMeshAgent agent;
    internal StateManager currentState;
    internal float commandDelay = 0.2f;
    private float currentDelay = 0;
    public float visibilityAngleCos = 0.5f;
    public float runAwayAngleCos = 0.8f;
    private bool isFleeing = false;

    void Awake()
    {
        // ��������� ���������� ������
        agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
        agent.updateRotation = true;
        agent.angularSpeed = 240; // ������ ����������� � �������, �� � �� ���� ������ ��������� � ������
    }

    public override void OnFixedTick()
    {
        if (currentDelay > 0)
        {
            currentDelay -= Time.fixedDeltaTime;
        }
    }

    internal void TryToDetect(Vector3 pos, Vector3 speed, float hearingRange)
    {
        Vector3 dir = pos - transform.position;
        if (Vector3.Dot(dir.normalized, transform.forward) > visibilityAngleCos) { 
            VisibleDetected(pos, speed);            
        }
        else if (dir.magnitude < hearingRange)
        {
            SoundDetected(pos, speed);
        } 
        else
        {
            Debug.Log("Not detected");
        }
    }

    internal void SoundDetected(Vector3 pos, Vector3 speed)
    {
        if (currentDelay > 0) return;
        //transform.forward = Vector3.Lerp(transform.forward, (pos - transform.position).normalized, 0.3f); // ��� � ��� ��������������; angular speed � ������; 
        currentDelay = commandDelay;

        Vector3 dir = transform.position - pos;
        if (isFleeing && Vector3.Dot(dir, speed) > dir.magnitude * dir.magnitude * runAwayAngleCos)
        {
            Debug.Log("���� �� ������ � ������");
            FleeSideways(dir, speed);
        }
        else
        {
            Debug.Log("�������, ��� ��� ������");
            agent.destination = pos;
            isFleeing = false;
        }
    }

    internal void VisibleDetected(Vector3 pos, Vector3 speed)
    {
        if (currentDelay > 0) return;
        currentDelay = commandDelay;
        Vector3 dir = transform.position - pos;
        if (Vector3.Dot(dir, speed) > dir.magnitude * dir.magnitude * runAwayAngleCos)
        {
            isFleeing = true;
            Debug.Log("������ �� ������");
            FleeSideways(dir, speed);
        }
        else
        {
            Debug.Log("������, ��� ��� ������");
            agent.destination = pos;
        }
    }

    private void FleeSideways(Vector3 dir, Vector3 speed)
    {
        Vector3 fleeDirection = new Vector3(speed.y, -speed.x, speed.z); // ���� �� ����������, ������ ��� � ������ �� � ���
        if (Vector3.Dot(dir, fleeDirection)>1) {
            fleeDirection*= -1;
        }
        agent.destination = transform.position + fleeDirection;
    }


}