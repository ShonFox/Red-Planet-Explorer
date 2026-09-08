using UnityEngine;

public class EnergyItem : MonoBehaviour
{
    [SerializeField] private int _researchValue = 25;


    private void OnTriggerEnter(Collider other)
    {

        if (other.attachedRigidbody.TryGetComponent<DronrEnergyCollector>(out var collector))
        {
            collector.Collect(_researchValue);
            Destroy(gameObject);
        }
        else
        {
            Debug.LogWarning("No collector found!");
        }
    }
}
