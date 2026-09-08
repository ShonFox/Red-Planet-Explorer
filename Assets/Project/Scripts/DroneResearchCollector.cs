using UnityEngine;

public class DroneResearchCollector : MonoBehaviour
{
    [SerializeField] private ResearchScore _researchScore;

    public void Collect(int researchValue)
    {
        _researchScore.AddPoints(researchValue);
    }
}
