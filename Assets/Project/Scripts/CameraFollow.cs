using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private Vector3 _offset = new Vector3(0f, 0f, -25f);

    [SerializeField] private float _smoothSpeed = 0.125f;

    private void LateUpdate()
    {
        if (_target != null)
        {
            Vector3 desiredPosition = _target.position + _offset;

            Vector3 smoothedPosition = Vector3.Lerp(
                transform.position,  // Текущая позиция
                desiredPosition,     // Конечная цель
                _smoothSpeed         // Скорость перехода
            );

            transform.position = smoothedPosition;
        }
    }
}
