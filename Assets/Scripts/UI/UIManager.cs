using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    [SerializeField] GameObject menuPanel;
    [SerializeField] TMP_Text menuText;

    [SerializeField] Image damageIndicator;
    private RectTransform floatingPanelPosition;

    private bool damageShow;

    void Start()
    {
        floatingPanelPosition = floatingPanel.GetComponent<RectTransform>();
    }

    public void HideDamageIndicator()
    {
        damageShow = false;
        damageIndicator.gameObject.SetActive(false);
    }
    public void ShowDamageIndicator()
    {
        damageShow = true;
        damageIndicator.color = new Color(1, 1, 1, 0.5f);
        damageIndicator.gameObject.SetActive(true);
    }
    public void FlashDamageIndicator()
    {
        damageIndicator.color = new Color(1, 1, 1, 0.5f);
        damageIndicator.gameObject.SetActive(true);
    }


    void Update()
    {
        if (damageShow == false && damageIndicator.gameObject.activeSelf)
        {
            damageIndicator.color = new Color(1, 1, 1, Math.Max(0, damageIndicator.color.a - Time.deltaTime * 1.5f));
            if (damageIndicator.color.a < 0.01f)
            {
                damageIndicator.gameObject.SetActive(false);
            }
        }
    }

    public void ShowMenu(string text)
    {
        menuText.text = text;
        menuPanel.SetActive(true);
    }

    public void ShowBottomPanel(string text)
    {
        bottomPanel.Show(text);
    }
    public void ShowTopPanel(string text)
    {
        topPanel.Show(text);
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
