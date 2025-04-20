
using UnityEngine;

public class Objective : MonoBehaviour
{
    private int enemiesKilled = 0;

    [SerializeField] int enemiesToKill = 3;

    bool isCompleted = false;
    void Start()
    {
        var ui = UIManager.Instance;
        ui.ShowBottomPanel("<color=\"yellow\">Objective: kill " + enemiesToKill + " enemies");
        EventManager.Instance.OnEnemyDie.AddListener(() =>
        {
            enemiesKilled++;

            if (enemiesKilled == enemiesToKill)
            {
                ui.ShowBottomPanel("<color=\"green\">Objective is completed!");
                isCompleted = true;
            }
            else
            {

                ui.ShowBottomPanel("<color=\"yellow\">You have killed an enemy!\n" + (enemiesToKill - enemiesKilled) + " more to go!");

            }
        });
    }

    private static Objective singleton = null;
    public static Objective Instance
    {
        get { return singleton == null ? (singleton = FindObjectOfType<Objective>()) : singleton; }
    }

    public bool IsCompleted()
    {
        return isCompleted;
    }


}

