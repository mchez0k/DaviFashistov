using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngineSound : MonoBehaviour
{
    AudioSource audioSource;
    public float minPitch = 0.2f;
    private float engineModifier;
    private CarController cc;

    public float baseDetectionRadius = 100f; // ������, � ������� ����� ����� ������� ����
    public LayerMask naziLayer; // ���� ��� ����������� �������

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.pitch = minPitch;
        cc = GetComponent<CarController>();
    }

    void FixedUpdate()
    {
        engineModifier = cc.carCurrentSpeed;
        if (engineModifier < minPitch)
            audioSource.pitch = minPitch;
        else
            audioSource.pitch = engineModifier;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, baseDetectionRadius * engineModifier, naziLayer); //�������������� �� � FixedUpdate
        foreach (var hitCollider in hitColliders)
        {
            Debug.Log($"��� ������ {hitCollider.name}");
            hitCollider.transform.root.TryGetComponent(out NaziAi naziBot);
            naziBot.goal = transform.position;
            // ����� ���������
        }
    }
}