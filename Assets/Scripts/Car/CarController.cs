using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public CarInput InputCtrl;
    [Tooltip("Set ref in order of FL, FR, RL, RR")]
    public WheelCollider[] WheelColliders;

    [Tooltip("Set ref of wheel meshes in order of  FL, FR, RL, RR")]
    public Transform[] Wheels;

    public Transform CenterOfMass;
    public Transform MainWheel;
    public Transform RotatePoint;
    public Transform SecondRotatePoint;
    private Vector3 needAxis;

    public int Force;
    public float minSteeringAngle = -27f;
    public float maxSteeringAngle = 27f;
    public float steeringSpeed = 10f;
    public int BrakeForce;

    public float carMaxSpeed = 100;
    public float carCurrentSpeed = 0;

    private float currentSteeringAngle = 0f;

    Rigidbody rb;

    private void Start()
    {
        //Time.timeScale = 0.5f;
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = CenterOfMass.localPosition;
    }

    private void FixedUpdate()
    {
        Steer();
        Drive();
        Brake();
        UpdateWheelMovements();
        UpdateMainWheel();
    }

    //private void OnCollisionEnter(Collision other)
    //{
    //    Debug.Log("colliderEnter");
    //    if (other.transform.root.TryGetComponent(out Ragdoll ragdoll)) // ��������� ��� ���������
    //    {
    //        Debug.Log("ragdoll");
    //        ragdoll.ToggleRagdoll(true);
    //        //other.GetComponent<CapsuleCollider>().enabled = false;
    //        //Destroy(GetComponent<CapsuleCollider>());

    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("colliderEnter");
        if (other.transform.root.TryGetComponent(out Ragdoll ragdoll)) // ��������� ��� ���������
        {
            Debug.Log("ragdoll");
            ragdoll.mainRb.AddForce(transform.forward * 100f, ForceMode.VelocityChange);
            ragdoll.ToggleRagdoll(true);
            //other.GetComponent<CapsuleCollider>().enabled = false;
            //Destroy(GetComponent<CapsuleCollider>());

        }
    }

    //Drive forward/backward
    private void Drive()
    {
        WheelColliders[2].motorTorque = InputCtrl.Vertical * Force;
    }

    //Steer left/right
    private void Steer()
    {
        carCurrentSpeed = (rb.velocity.magnitude * 3.6f) / carMaxSpeed;
        float targetSteeringAngle = maxSteeringAngle * Input.GetAxis("Horizontal");
        currentSteeringAngle = Mathf.Lerp(currentSteeringAngle, targetSteeringAngle, Time.deltaTime * steeringSpeed);

        // ����������� ���� ��������
        currentSteeringAngle = Mathf.Clamp(currentSteeringAngle, minSteeringAngle, maxSteeringAngle);

        WheelColliders[0].steerAngle = WheelColliders[1].steerAngle = currentSteeringAngle;
    }

    private void UpdateMainWheel()
    {
        //float rotationAmount = currentSteeringAngle * Time.deltaTime * steeringSpeed; // ���������������� �������� ��������
        //MainWheel.Rotate(needAxis, rotationAmount); // ������� ������ ��� Z, ����������� ����

        needAxis = (SecondRotatePoint.position - RotatePoint.position).normalized;
        Quaternion rotation = Quaternion.AngleAxis(currentSteeringAngle, needAxis);

        // ���������� �������� � �������
        MainWheel.rotation = rotation * MainWheel.rotation; // ��������� ������������
    }

    //Apply brakes
    private void Brake()
    {
        WheelColliders[2].brakeTorque = InputCtrl.Brake * BrakeForce;
    }

    //imitate the wheelcollider movements onto the wheel-meshes
    private void UpdateWheelMovements()
    {
        for (var i = 0; i < Wheels.Length; i++)
        {
            Vector3 pos;
            Quaternion rot;
            WheelColliders[i].GetWorldPose(out pos, out rot);
            Wheels[i].transform.position = pos;
            Wheels[i].transform.rotation = rot;
        }
    }
}