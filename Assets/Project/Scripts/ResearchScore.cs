using TMPro;
using UnityEngine;

public class ResearchScore : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _scoreText1;

    private int _currentScore;

    private void Start()
    {
        SetAndDisplay(0);
    }

    public void AddPoints(int amount)
    {
        int absScore = Mathf.Abs(amount);

        int newScore = _currentScore + absScore;

        SetAndDisplay(newScore);
    }

    private void SetAndDisplay(int newValue)
    {
        _currentScore = newValue;
        Display();
    }

    private void Display()
    {
        _scoreText.text = $"{_currentScore}";
        _scoreText1.text = $"{_currentScore}";
    }
}
