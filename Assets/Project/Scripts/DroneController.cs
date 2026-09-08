using UnityEngine;
using UnityEngine.InputSystem;

public class DroneController : MonoBehaviour
{
    [SerializeField] private DroneEnergy _droneEnergy;

    [SerializeField] private Transform _leftEnginePosition;

    [SerializeField] private Transform _rightEnginePosition;

    [SerializeField] private Rigidbody _rigidbody;

    [SerializeField] private float _mainThrust = 10f;

    [SerializeField] private float _attitudeThrust = 5f;

    [SerializeField] private float _energyConsumptionRate = 2f;

    [SerializeField] private float _maxHeight = 10f;

    private Vector2 _direction;

    public Vector3 Velocity => _rigidbody.linearVelocity;

    public bool IsInteractive { get; set; }

    private void Awake()
    {
        IsInteractive = true;
        _direction = Vector2.zero;
    }

    private void Start()
    {
        _rigidbody.sleepThreshold = 0.0f;
    }

    private void FixedUpdate()
    {
        if (IsInteractive == false) return;
        ApplyEngineForces();
    }

    public void OnMove(InputValue value)
    {
        _direction = value.Get<Vector2>();
    }

    private void ApplyEngineForces()
    {
        // Левый двигатель: основная тяга + коррекция направления
        float leftThrust = CalculateThrust(_direction.x, _direction.y);

        // Правый двигатель: инвертированное направление по X
        float rightThrust = CalculateThrust(_direction.x * -1, _direction.y);

        // Применяем силы к соответствующим точкам
        ApplyEngineForce(leftThrust, _leftEnginePosition.position);
        ApplyEngineForce(rightThrust, _rightEnginePosition.position);
    }

    private float CalculateThrust(float horizontalInput, float verticalInput)
    {
        // Комбинируем основную тягу и коррекцию
        float thrustValue = _mainThrust * verticalInput + _attitudeThrust * horizontalInput;

        // Ограничиваем минимальным значением
        float actualThrust = Mathf.Clamp(thrustValue, 0, float.MaxValue);

        return actualThrust;
    }

    private void ApplyEngineForce(float thrustValue, Vector3 enginePosition)
    {
        // Проверяем, хватает ли энергии
        if (_droneEnergy.TryConsumeEnergy(thrustValue * _energyConsumptionRate))
        {
            // Рассчитываем коэффициент мощности в зависимости от высоты
            float powerFactor = CalculatePowerFactor();

            // Корректируем тягу с учётом коэффициентом мощности
            float actualThrust = thrustValue * powerFactor;

            // Применяем силу к твёрдому телу объекта
            _rigidbody.AddForceAtPosition(
                transform.up * actualThrust,     // Направление тяги
                enginePosition,                  // Точка приложения
                ForceMode.Acceleration          // Режим применения силы
            );
        }
    }

    private float CalculatePowerFactor()
    {
        // Текущая высота дрона
        float currentHeight = transform.position.y;

        // Относительная высота (0 - на земле, 1 - на максимальной высоте)
        float heightRatio = Mathf.Clamp01((_maxHeight - currentHeight) / _maxHeight);

        // Квадратичная интерполяция для плавного снижения мощности
        return heightRatio * heightRatio;
    }
}
