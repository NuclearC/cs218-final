using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{

    public UnityEvent OnEnemyDie { get; set; }
    public UnityEvent OnPlayerHit { get; set; }
    public UnityEvent OnPlayerShoot { get; set; }

    private static EventManager eventManager = null;
    public static EventManager Instance
    {
        get { return eventManager == null ? (eventManager = FindObjectOfType<EventManager>()) : eventManager; }
    }

    void Awake()
    {
        OnEnemyDie = new UnityEvent();
    }
}
