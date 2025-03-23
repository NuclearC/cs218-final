using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public InputHandler InputHandler { get; private set; }

    private static GameManager singleton;
    public static GameManager Instance
    {
        get { return singleton ? singleton : (singleton = FindObjectOfType<GameManager>()); }
    }
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
}
