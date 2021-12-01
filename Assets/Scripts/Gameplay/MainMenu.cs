using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        if (MusicManager._instance != null)
        {
            MusicManager._instance.PlayStartGame();
        }
        SceneManager.LoadScene(1);
    }
}
