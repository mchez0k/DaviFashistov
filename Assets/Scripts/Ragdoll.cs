using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Rigidbody[] bodyRigidbodies;
    [SerializeField] private Animator animator;
    public Rigidbody mainRb;
    private void Start()
    {
        // ������ ��� Rigidbody � ������������ �������
        bodyRigidbodies = GetComponentsInChildren<Rigidbody>();
        //foreach (var body in bodyRigidbodies)
        //{
        //    body.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        //}
        animator = GetComponent<Animator>();
        mainRb = GetComponent<Rigidbody>();

        // ������� �������� ��� Rigidbody ��� ragdoll
        ToggleRagdoll(false);
    }

    internal void ToggleRagdoll(bool isActive)
    {
        // ��������� ��������, ����� ragdoll �����������
        if (animator != null)
        {
            animator.enabled = !isActive; // ��������� Animator, ���� isActive = true
        }
        foreach (Rigidbody rb in bodyRigidbodies)
        {
            rb.isKinematic = !isActive; // ��������/��������� ������
        }
    }
}
