using UnityEngine;
using UnityEngine.SceneManagement;
public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private PlayerPoints playerPoints;

    [SerializeField]
    private GameObject GameCanvas;

    [SerializeField]
    private GameObject PauseCanvas;

    void Start()
    {
        playerPoints.ResetPoints();
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        GameCanvas.SetActive(false);
        PauseCanvas.SetActive(true);
    }

    public void ContinueGame()
    {
        Time.timeScale = Random.Range(0.8f, 2.0f);
        GameCanvas.SetActive(true);
        PauseCanvas.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}
