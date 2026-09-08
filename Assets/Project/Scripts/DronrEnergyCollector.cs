using UnityEngine;

public class DronrEnergyCollector : MonoBehaviour
{
    [SerializeField] private DroneEnergy _energyScore;

    public void Collect(int researchValue)
    {
        _energyScore.AddEnergy(researchValue);
    }
}
