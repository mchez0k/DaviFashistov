using System.Collections.Generic;
using UnityEngine;

public class CarHealth : MonoCache
{
    [SerializeField] private float carHp = 100f;
    [SerializeField] private float currentCarHp;
    [SerializeField] private float carFuel = 100f;
    [SerializeField] private float currentCarFuel;
    [SerializeField] private float carFuelConsumption = 1f;

    public float CurrentCarHp => currentCarHp; // �������� ��� �������� ��������
    public float MaxCarHp => carHp; // �������� ��� ������������� ��������
    public float CurrentCarFuel => currentCarFuel; // �������� ��� �������� �������
    public float MaxCarFuel => carFuel; // �������� ��� ������������� �������

    private List<ICarObserver> observers = new List<ICarObserver>();

    void Awake()
    {
        currentCarFuel = carFuel;
        currentCarHp = carHp;
    }

    public override void OnFixedTick()
    {
        FuelControl();
        NotifyFuelChange(); // ���������� ������������ � ��������� �������
    }

    private void FuelControl()
    {
        currentCarFuel -= carFuelConsumption * Time.deltaTime; // ������ �������
        if (currentCarFuel < 0) currentCarFuel = 0;
    }

    public void Attach(ICarObserver observer)
    {
        observers.Add(observer);
    }

    public void Detach(ICarObserver observer)
    {
        observers.Remove(observer);
    }

    private void NotifyFuelChange()
    {
        foreach (var observer in observers)
        {
            observer.OnFuelChanged(currentCarFuel);
        }
    }

    public void TakeDamage(float damage)
    {
        currentCarHp -= damage;
        if (currentCarHp < 0) currentCarHp = 0; // ������������ ��������
        NotifyHealthChange(); // ���������� ������������ � ��������� ��������
    }

    private void NotifyHealthChange()
    {
        foreach (var observer in observers)
        {
            observer.OnHealthChanged(currentCarHp);
        }
    }
}