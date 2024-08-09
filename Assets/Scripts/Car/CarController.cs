using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoCache
{
    private CarInput InputCtrl;

    [Header("Колёса")]
    public WheelCollider[] WheelColliders;
    public Transform[] Wheels;
    [Space(10)]

    [Header("Позиции")]
    [SerializeField] private Transform CenterOfMass;
    [SerializeField] private Transform MainWheel;
    [SerializeField] private Transform SecondRotatePoint;
    [SerializeField] private Transform Camera;
    [Space(10)]

    [Header("Звуки")]
    [SerializeField] private EngineSound engineSound;
    [SerializeField] private AudioClip sound;
    [SerializeField] private AudioSource audioSource;

    private Vector3 needAxis;
    [Space(10)]

    [Header("Настройки машины")]
    [SerializeField] private float force;
    [SerializeField] private float brakeForce;
    [Space(2)]
    [SerializeField] private float kickForce = 1f;

    [Space(2)]
    [SerializeField] private float carMaxSpeed = 100;
    [SerializeField] internal float carCurrentSpeed = 0;
    [Space(2)]
    [SerializeField] private float minSteeringAngle = -27f;
    [SerializeField] private float maxSteeringAngle = 27f;
    [SerializeField] private float steeringSpeed = 10f;

    private float currentSteeringAngle = 0f;
    private Vector3 initialCameraPosition;

    Rigidbody rb;

    private void Awake()
    {
        engineSound = GetComponent<EngineSound>();
        InputCtrl = GetComponent<CarInput>();
        rb = GetComponent<Rigidbody>();

        rb.centerOfMass = CenterOfMass.localPosition;

        //audioSource.Play();
    }

    public override void OnFixedTick()
    {
        Steer();
        Drive();
        Brake();
        UpdateWheelMovements(); //включить, когда разделим колёса
        UpdateMainWheel();

        CameraControl();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.TryGetComponent(out Ragdoll ragdoll) && !ragdoll.isDead) // Проверяем тег коллидера
        {
            var direction = other.transform.position - transform.position;
            direction.y -= 2f;
            ragdoll.ToggleRagdoll(true);
            if (carCurrentSpeed < 0.5)
            {
                kickForce = 1f;
            } else
            {
                kickForce = 2f * carCurrentSpeed;
            }
            ragdoll.LaunchRaggdol(kickForce, direction); // Запускаем в космос
            ragdoll.isDead = true;
            BaseSpawner.currentEnemy--;
        }
    }

    #region Handle
    //Drive forward/backward
    private void Drive()
    {
        carCurrentSpeed = (rb.velocity.magnitude * 5f) / carMaxSpeed;
        if (carCurrentSpeed > 1) return;
        WheelColliders[2].motorTorque = WheelColliders[3].motorTorque = InputCtrl.Vertical * force;
    }

    //Steer left/right
    private void Steer()
    {
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

        needAxis = (SecondRotatePoint.position - MainWheel.position).normalized;
        Quaternion rotation = Quaternion.AngleAxis(currentSteeringAngle, needAxis);

        // Применение вращения к объекту
        MainWheel.rotation = rotation * MainWheel.rotation; // Умножение кватернионов
    }

    //Apply brakes
    private void Brake()
    {
        WheelColliders[2].brakeTorque = WheelColliders[2].brakeTorque = InputCtrl.Brake * brakeForce;
    }

    private void CameraControl()
    {
        // Рассчитываем новую позицию камеры в зависимости от текущей скорости
        float speedFactor = carCurrentSpeed / carMaxSpeed;
        Vector3 targetPosition = initialCameraPosition - new Vector3(0, 0, speedFactor * 20); // 10 - это максимальное смещение камеры

        // Плавно изменяем позицию камеры
        Camera.localPosition = Vector3.Lerp(Camera.localPosition, targetPosition, Time.deltaTime * 10); // 2 - это скорость интерполяции
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
    #endregion
}