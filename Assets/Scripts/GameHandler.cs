using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;

public class GameHandler : MonoBehaviour
{
    [SerializeField] public static GameHandler instance;
    [SerializeField] PlayerInput playerControls;
    [SerializeField] public PlayerController player;
    [SerializeField] GameOverFade gameOverFade;

    private void Awake()
    {
        instance = this;
        activeSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Start()
    {
        player?.OnDeath.AddListener(StartGameOver);
        gameOverFade?.FadeDone.AddListener(GoToGameOver);
    }

    #region levelManagement
    public bool LevelStarted = true;
    bool gameOverStarted = false;

    public void StartGameOver()
    {
        if(!gameOverStarted)
        {
            gameOverStarted = true;

            playerControls.currentActionMap.Disable();

            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            gameOverFade.FadeScreen();
            //Waits for gameOverFade.FadeDone before proceeding with GoToGameOver
        }
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
