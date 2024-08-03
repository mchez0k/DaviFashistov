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

    [SerializeField] private AudioClip sound;
    [SerializeField] private AudioSource audioSource;

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
        audioSource.Play();
    }

    private void FixedUpdate()
    {
        Steer();
        Drive();
        Brake();
        UpdateWheelMovements();
        UpdateMainWheel();


    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.TryGetComponent(out Ragdoll ragdoll)) // Проверяем тег коллидера
        {
            ragdoll.LaunchRaggdol(1f, transform.forward);
            ragdoll.ToggleRagdoll(true);
            ragdoll.isDead = true;
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

        // Ограничение угла поворота
        currentSteeringAngle = Mathf.Clamp(currentSteeringAngle, minSteeringAngle, maxSteeringAngle);

        WheelColliders[0].steerAngle = WheelColliders[1].steerAngle = currentSteeringAngle;
    }

    private void UpdateMainWheel()
    {
        //float rotationAmount = currentSteeringAngle * Time.deltaTime * steeringSpeed; // Пропорциональное значение поворота
        //MainWheel.Rotate(needAxis, rotationAmount); // Вращаем вокруг оси Z, инвертируем знак

        needAxis = (SecondRotatePoint.position - RotatePoint.position).normalized;
        Quaternion rotation = Quaternion.AngleAxis(currentSteeringAngle, needAxis);

        // Применение вращения к объекту
        MainWheel.rotation = rotation * MainWheel.rotation; // Умножение кватернионов
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