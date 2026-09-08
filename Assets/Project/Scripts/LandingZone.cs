using UnityEngine;

public class LandingZone : MonoBehaviour
{
    [SerializeField] private GameManager _gameManager;

    [SerializeField] private float _maxAngle = 5f;

    [SerializeField] private float _minSpeed = 0.1f;

    [SerializeField] private float _landingTime = 2f;

    private float _timer;

    private void OnCollisionStay(Collision collision)
    {
        Debug.Log($"Collision {collision.gameObject.name} detected!");

        DroneController drone = collision.gameObject.GetComponent<DroneController>();

        if (drone == null) return;

        if (drone.IsInteractive == false) return;

        if (IsLandingConditionsMet(drone))
        {
            _timer += Time.deltaTime;

            if (_timer >= _landingTime)
            {
                CompleteLanding(drone);
            }
        }
        else
        {
            _timer = 0f;
        }
    }

    private bool IsLandingConditionsMet(DroneController drone)
    {
        bool speedCondition = drone.Velocity.magnitude < _minSpeed;

        bool angleCondition = Vector3.Angle(Vector3.up, drone.transform.up) <= _maxAngle;

        return speedCondition && angleCondition;
    }

    private void CompleteLanding(DroneController drone)
    {
        drone.IsInteractive = false;

        _gameManager.OnWin();
    }
}
