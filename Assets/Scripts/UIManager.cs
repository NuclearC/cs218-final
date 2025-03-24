using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager singleton;
    public static UIManager Instance
    {
        get { return singleton ? singleton : (singleton = FindObjectOfType<UIManager>()); }
    }

    [SerializeField] TMP_Text debugText;

    void Start()
    {

    }


    void Update()
    {

    }

    public void UpdateInfoText(int health, int ammo, int ammoTotal, string currentWeapon, string[] inventory)
    {
        debugText.text = "HEALTH: " + health + "/100 \n"
            + "AMMO: " + ammo + "/" + ammoTotal + "\n"
            + "WEAPON: " + currentWeapon + "\n"
            + "INVENTORY: " + string.Join(",", inventory);
    }
}
