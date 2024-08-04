using UnityEngine;
using UnityEngine.UI;

public class CarUI : MonoBehaviour, ICarObserver
{
    public Image healthBar;
    public Image fuelBar;

    private CarHealth carHealth;

    public void Awake()
    {
        carHealth = GetComponent<CarHealth>();
        carHealth.Attach(this);
        UpdateUI();
    }

    public void OnHealthChanged(float newHealth)
    {
        UpdateUI();
    }

    public void OnFuelChanged(float newFuel)
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        healthBar.fillAmount = carHealth.CurrentCarHp / carHealth.MaxCarHp;
        fuelBar.fillAmount = carHealth.CurrentCarFuel / carHealth.MaxCarFuel;
    }

    private void OnDestroy()
    {
        if (carHealth != null)
        {
            carHealth.Detach(this);
        }
    }
}