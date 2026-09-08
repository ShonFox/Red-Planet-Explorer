using UnityEngine;

public class DangerMove : MonoBehaviour
{
    // Пороговое расстояние для определения достижения точки (в метрах)
    private const float _arrivalThreshold = 0.01f;

    // Массив точек назначения, между которыми объект будет перемещаться
    [SerializeField] private Transform[] _waypoints;

    // Ссылка на твёрдое тело (Rigidbody) объекта
    [SerializeField] private Rigidbody _rigidBody;

    // Скорость движения объекта в юнитах (м/с)
    [SerializeField] private float _moveSpeed = 2.0f;

    // Индекс текущей точки назначения
    private int _targetWaypointIndex;

    /// <summary>
    /// Метод вызывается с фиксированным интервалом
    /// перед тем, как будет запущен физический движок
    /// </summary>
    private void FixedUpdate()
    {
        // Перемещение к целевой позиции
        MoveToTargetWaypoint();

        // Если целевая позиция достигнута
        if (HasArrivedToWaypoint())
        {
            // Назначаем следующую целевую позицию
            SwitchToNextTargetIndex();
        }
    }

    private void MoveToTargetWaypoint()
    {
        // Рассчитываем расстояние, на которое нужно сместиться
        // за интервал между вызовами FixedUpdate
        float deltaDistance = _moveSpeed * Time.fixedDeltaTime;

        // Получаем позицию целевой точки
        Vector3 targetPosition = _waypoints[_targetWaypointIndex].position;

        // Вычисляем новую позицию как промежуточную точку между текущей позицией и целью,
        // перемещая объект на дистанцию deltaDistance, но не дальше цели
        Vector3 newPosition = Vector3.MoveTowards(_rigidBody.position, targetPosition, deltaDistance);

        // Обновляем позицию твёрдого тела объекта
        _rigidBody.MovePosition(newPosition);
    }

    /// <summary>
    /// Проверяет достижение текущей целевой позиции
    /// </summary>
    /// <returns>True, если объект находится достаточно близко к цели</returns>
    private bool HasArrivedToWaypoint()
    {
        // Получаем позицию текущей целевой точки
        Vector3 targetPosition = _waypoints[_targetWaypointIndex].position;

        // Сравниваем расстояние до цели с пороговым значением (0.01f)
        // Используется небольшое значение для компенсации ошибок округления
        return Vector3.Distance(_rigidBody.position, targetPosition) < _arrivalThreshold;
    }

    /// <summary>
    /// Устанавливает индекс следующей целевой позиции
    /// </summary>
    private void SwitchToNextTargetIndex()
    {
        // Переходим к следующей точке маршрута
        // Оператор % (остаток от деления) позволяет циклически возвращаться к началу массива
        // Например: при массиве из 4 точек и индексе 3, (3+1)%4 = 0, возвращаемся к первой точке        
        _targetWaypointIndex = (_targetWaypointIndex + 1) % _waypoints.Length;
    }
}
