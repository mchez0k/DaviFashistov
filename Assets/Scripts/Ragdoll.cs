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
        // Найдем все Rigidbody в родительском объекте
        bodyRigidbodies = GetComponentsInChildren<Rigidbody>();
        //foreach (var body in bodyRigidbodies)
        //{
        //    body.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        //}
        animator = GetComponent<Animator>();
        mainRb = GetComponent<Rigidbody>();

        // Сначала выключим все Rigidbody для ragdoll
        ToggleRagdoll(false);
    }

    internal void ToggleRagdoll(bool isActive)
    {
        // Отключаем анимации, когда ragdoll активирован
        if (animator != null)
        {
            animator.enabled = !isActive; // Отключаем Animator, если isActive = true
        }
        foreach (Rigidbody rb in bodyRigidbodies)
        {
            rb.isKinematic = !isActive; // Включаем/выключаем физику
        }
    }
}
