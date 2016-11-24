using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayQuit : MonoBehaviour {
    public int GameSceneIndex = 1;
    public void GOTOGAME()
    {
        SceneManager.LoadScene(GameSceneIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
