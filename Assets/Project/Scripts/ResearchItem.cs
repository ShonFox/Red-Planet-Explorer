using UnityEngine;

public class ResearchItem : MonoBehaviour
{
    [SerializeField] private int _researchValue = 25;
    

    private void OnTriggerEnter(Collider other)
    {

        if (other.attachedRigidbody.TryGetComponent<DroneResearchCollector>(out var collector))
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
