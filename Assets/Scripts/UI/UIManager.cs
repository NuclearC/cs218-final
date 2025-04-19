using System.Collections;
using System.Collections.Generic;
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

    [SerializeField] PanelBehavior topPanel;
    [SerializeField] PanelBehavior bottomPanel;
    [SerializeField] PanelBehavior floatingPanel;
    private RectTransform floatingPanelPosition;

    void Start()
    {
        topPanel.Show("Use the 1,2,3,4 keys to switch the main weapon");
        //bottomPanel.Show("wtf");

        floatingPanelPosition = floatingPanel.GetComponent<RectTransform>();
    }


    void Update()
    {

    }

    public void HideFloatingPanel()
    {
        floatingPanel.Hide();
    }
    public void ShowFloatingPanel(string text, Vector2 position)
    {
        floatingPanel.Show("<color=\"white\">Press <color=\"yellow\">E<color=\"white\"> to equip <color=\"yellow\">" + text);

        floatingPanelPosition.position = position;
    }

    public void UpdateInfoText(int health, int ammo, int ammoTotal, string currentWeapon, string[] inventory)
    {
        debugText.text = "HEALTH: " + health + "/100 \n"
            + "AMMO: " + ammo + "/" + ammoTotal + "\n"
            + "WEAPON: " + currentWeapon + "\n"
            + "INVENTORY: " + string.Join(",", inventory);
    }
}
