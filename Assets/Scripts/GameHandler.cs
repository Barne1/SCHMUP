using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;

public class GameHandler : MonoBehaviour
{
    [SerializeField] public static GameHandler instance;
    [SerializeField] PlayerInput playerControls;
    [SerializeField] GameOverFade gameOverFade;

    private void Awake()
    {
        instance = this;
        activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    #region levelManagement
    public bool levelStarted { get; set; } = true;
    bool gameOverStarted = false;
    [System.NonSerialized]public bool gameOverDone = false;

    public void StartGameOver()
    {
        if(!gameOverStarted)
        {
            gameOverStarted = true;

            playerControls.currentActionMap.Disable();

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            StartCoroutine(GameOverTimer());
        }
    }

    IEnumerator GameOverTimer()
    {
        gameOverFade.FadeScreen();
        yield return new WaitUntil(() => gameOverDone);
        GoToGameOver();
    }

    #endregion levelManagement

    #region SceneManagement
    const int mainMenu = 0;
    int activeSceneIndex;

    public void NextScene()
    {
        SceneManager.LoadScene(activeSceneIndex + 1);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(mainMenu);
    }

    void GoToGameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion SceneManagement
}
