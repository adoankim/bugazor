using UnityEngine;
using UnityEngine.SceneManagement;
public class GameplayManager : MonoBehaviour
{
    [SerializeField]
    private PlayerPoints playerPoints;

    [SerializeField]
    private PlayerDamage playerDamage;

    [SerializeField]
    private GameObject GameCanvas;

    [SerializeField]
    private GameObject PauseCanvas;

    [SerializeField]
    private GameObject GameOverCanvas;

    public static PlayerPoints playerPointsRef;

    void Awake()
    {
        playerPointsRef = playerPoints;
        playerPoints.ResetPoints();
        playerDamage.AddOnHealthChangedListener(OnPlayerHealthUpdated);
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

    public void GameOver()
    {
        Time.timeScale = 0;
        GameCanvas.SetActive(false);
        PauseCanvas.SetActive(false);
        GameOverCanvas.SetActive(true);
    }

    void OnPlayerHealthUpdated(int playerLives)
    {
        if(playerLives > 0)
        {
            return;
        }

        GameOver();
    }
}
