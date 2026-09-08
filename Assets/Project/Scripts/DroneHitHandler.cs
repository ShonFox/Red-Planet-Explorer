using UnityEngine;

public class DroneHitHandler : MonoBehaviour
{
    [SerializeField] private DroneDestructor _destructor;

    [SerializeField] private float destructionSpeedThreshold = 3f;

    private void OnCollisionEnter(Collision collision)
    {
        float impactSpeed = collision.relativeVelocity.magnitude;

        if (impactSpeed > destructionSpeedThreshold)
        {
            _destructor.Detonate();
        }
    }
}
