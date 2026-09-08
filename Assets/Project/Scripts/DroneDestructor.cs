using UnityEngine;
using static Unity.VisualScripting.Metadata;

public class DroneDestructor : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private float _explosionRadius = 5f;
    [SerializeField] private float _explosionForce = 10f;
    [SerializeField] private float _upwardsModifier = 0f;

    private bool _isDestroyed;
    public bool IsDestroyed => _isDestroyed;

    private void Awake()
    {
        // Инициализация начального состояния
        _isDestroyed = false;
    }

    public void Detonate()
    {
        if (_isDestroyed == true) return;
        Explode();
        AfterExplode();
    }

    private void Explode()
    {
        // Получаем все дочерние объекты (включая неактивные)
        Transform[] children = GetComponentsInChildren<Transform>(true);

        // Обходим все дочерние объекты
        foreach (Transform child in children)
        {
            // Пропускаем родительский объект
            if (child == transform) continue;

            // Отсоединяем дочерний объект от родителя
            child.parent = null;

            // Инициализируем физику для детали
            Rigidbody childRigidbody = InitializeChildPhysics(child.gameObject);

            // Применяем силу взрыва
            ApplyExplosionForce(childRigidbody);
        }

        // Уведомляем GameManager о поражении
        _gameManager.OnLose();
    }

    private Rigidbody InitializeChildPhysics(GameObject childObject)
    {
        // Создаём Rigidbody для детали
        Rigidbody childRigidbody = childObject.AddComponent<Rigidbody>();

        // Запрещаем движение по оси Z и вращение по осям X и Y
        childRigidbody.constraints =
            RigidbodyConstraints.FreezePositionZ
            | RigidbodyConstraints.FreezeRotationX
            | RigidbodyConstraints.FreezeRotationY;

        return childRigidbody;
    }

    private void ApplyExplosionForce(Rigidbody childRigidbody)
    {
        // Применяем взрывную силу к детали
        childRigidbody.AddExplosionForce(
            _explosionForce,          // Сила взрыва
            transform.position,       // Центр взрыва
            _explosionRadius,         // Радиус действия
            _upwardsModifier,         // Вертикальный модификатор
            ForceMode.Impulse         // Режим приложения силы
        );
    }

    private void AfterExplode()
    {
        // Уничтожаем контейнерный объект
        Destroy(gameObject);

        // Маркируем как разрушенный, чтобы избежать повторного взрыва
        _isDestroyed = true;
    }
}
