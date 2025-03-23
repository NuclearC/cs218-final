using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShortcutManagement;
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
}
