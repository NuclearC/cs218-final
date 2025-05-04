using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public InputHandler InputHandler { get; private set; }

    private static GameManager singleton;
    public static GameManager Instance
    {
        get { return singleton ? singleton : (singleton = FindObjectOfType<GameManager>()); }
    }
    public bool IsPaused { get; private set; }
    void Awake()
    {
        InputHandler = GetComponent<InputHandler>();
    }

    public void CursorLock()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void CursorUnlock()
    {
        Cursor.lockState = CursorLockMode.None;
    }

    // perform all the necessary tasks when the game starts
    // (e.g. lock the cursor, reset the score and stats, etc)
    public void GameStart()
    {
        // TODO: implement
    }

    // perform the game end tasks (e.g. save)
    public void GameEnd()
    {
        // TODO: implement
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Pause()
    {
        CursorUnlock();
        IsPaused = true;
        Time.timeScale = 0;
        var ui = UIManager.Instance;
        ui.ShowMenu("Paused", true);
    }
    public void Resume()
    {
        CursorLock();
        IsPaused = false;
        Time.timeScale = 1;
        var ui = UIManager.Instance;
        ui.HideMenu();
    }
}
