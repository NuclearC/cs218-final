
using System.Linq;
using UnityEngine;

public class Objective : MonoBehaviour
{
    private int enemiesKilled = 0;

    private int enemiesToKill = 0;

    bool isCompleted = false;
    void Start()
    {
        enemiesToKill = FindObjectsByType<EnemyBehavior>(FindObjectsSortMode.None).Count();
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

