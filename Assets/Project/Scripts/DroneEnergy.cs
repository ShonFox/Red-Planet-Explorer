using TMPro;
using UnityEngine;

public class DroneEnergy : MonoBehaviour
{
    [SerializeField] private TMP_Text _energyText;
    [SerializeField] private float _maxEnergy = 300.0f;

    private float _currentEnergy;

    public float CurrentEnergy => _currentEnergy;

   private void Start()
    {
        SetAndDisplay(_maxEnergy);
    }

    public void AddEnergy(float amount)
    {
        float newEnergy = _currentEnergy + Mathf.Abs(amount);

        SetAndDisplay(newEnergy);
    }

    public bool TryConsumeEnergy(float energyPerSecond)
    {
        energyPerSecond = Mathf.Abs(energyPerSecond);

        if (_currentEnergy > 0)
        {
            float requiredEnergy = energyPerSecond * Time.fixedDeltaTime;

            float newEnergy = _currentEnergy - requiredEnergy;

           SetAndDisplay(newEnergy);

            return true;
        }

        return false;
    }

    private void SetAndDisplay(float newEnergy)
    {
        _currentEnergy = Mathf.Clamp(newEnergy, 0f, _maxEnergy);

        Display();
    }

   private void Display()
    {
        _energyText.text = $"{Mathf.Round(_currentEnergy * 100f) / 100f:F2}";
    }
}
