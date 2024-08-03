using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ragdoll : MonoBehaviour
{
    [SerializeField] private Rigidbody[] bodyRigidbodies;
    [SerializeField] private Animator animator;

    [SerializeField] private AudioClip[] sounds;
    [SerializeField] private AudioSource audioSource;

    public bool isDead = false;
    private void Start()
    {
        bodyRigidbodies = GetComponentsInChildren<Rigidbody>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

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

    internal void LaunchRaggdol(float force, Vector3 direction)
    {
        foreach (var item in bodyRigidbodies)
        {
            item.AddForce(force * direction, ForceMode.Impulse);
        }
        PlayRandomSound();
    }

    private void PlayRandomSound()
    {
        if (isDead) return; // ���� ������� � LaunchRaggdol, �� ��� ����������� ��������
        if (sounds.Length > 0)
        {
            AudioClip randomClip = sounds[Random.Range(0, sounds.Length)];
            audioSource.PlayOneShot(randomClip);
        }
    }
}
