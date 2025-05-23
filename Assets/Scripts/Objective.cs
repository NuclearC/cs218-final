
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Objective : MonoBehaviour
{
    private int enemiesKilled = 0;

    [SerializeField] int enemiesToKill = 0;

    [SerializeField] string text = "";

    [SerializeField] bool showEnemiesLeft = true;

    bool isCompleted = false;
    void Start()
    {
        int tmp = FindObjectsByType<EnemyBehavior>(FindObjectsSortMode.None).Count();
        if (tmp > 0)
            enemiesToKill = tmp;
        var ui = UIManager.Instance;

        string objectiveText = "";
        if (text != "")
            ui.ShowBottomPanel(objectiveText = "<color=\"yellow\">Objective: kill " + enemiesToKill + " enemies and \n <color=\"red\">" + text);
        else
            ui.ShowBottomPanel(objectiveText = "<color=\"yellow\">Objective: kill " + enemiesToKill + " enemies");
        EventManager.Instance.OnEnemyDie.AddListener(() =>
        {
            enemiesKilled++;

            if (enemiesKilled == enemiesToKill)
            {
                ui.ShowBottomPanel("<color=\"green\">Objective is completed!");
                isCompleted = true;

                EventManager.Instance.OnObjectiveCompleted.Invoke();
            }
            else
            {
                ui.ShowBottomPanel("<color=\"yellow\">You have killed an enemy!\n" + (showEnemiesLeft ? (enemiesToKill - enemiesKilled) + " more to go!" : ""));
            }
        });

        ui.SetObjective(objectiveText);
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

