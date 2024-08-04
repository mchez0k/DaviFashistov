public interface ICarObserver
{
    void OnHealthChanged(float newHealth);
    void OnFuelChanged(float newFuel);
}