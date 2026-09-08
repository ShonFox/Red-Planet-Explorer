using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _winPanel;
    [SerializeField] private GameObject _losePanel;
    [SerializeField] private float _winDelay = 1f;
    [SerializeField] private float _loseDelay = 2f;

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnWin()
    {
        StartCoroutine(ShowPanel(_winPanel, _winDelay));
    }

    public void OnLose()
    {
       StartCoroutine(ShowPanel(_losePanel, _loseDelay));
    }

    private IEnumerator ShowPanel(GameObject panel, float delay)
    {
        yield return new WaitForSeconds(delay);

        panel.SetActive(true);
    }
}
