using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
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
    [SerializeField] TMP_Text objectiveText;

    [SerializeField] PanelBehavior topPanel;
    [SerializeField] PanelBehavior bottomPanel;
    [SerializeField] PanelBehavior floatingPanel;
    [SerializeField] GameObject menuPanel;
    [SerializeField] GameObject menuContinuteButton;
    [SerializeField] TMP_Text menuText;

    [SerializeField] Image damageIndicator;
    [Header("HUD/Health")]
    [SerializeField] Image healthBar;
    [SerializeField] TMP_Text healthText;
    [Header("HUD/Ammo")]
    [SerializeField] Image ammoBar;
    [SerializeField] TMP_Text ammoText;

    [Header("HUD/Arrow")]
    [SerializeField] Image arrow;
    [SerializeField] float arrowRadius;

    private float arrowCurrentAngle;

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

    public void ShowArrow(float angle)
    {
        arrow.gameObject.SetActive(true);
        arrowCurrentAngle = Mathf.LerpAngle(arrowCurrentAngle, angle, Time.deltaTime * 20.0f);
        arrow.rectTransform.anchoredPosition = new Vector2(Mathf.Cos(Mathf.Deg2Rad * arrowCurrentAngle), Mathf.Sin(Mathf.Deg2Rad * arrowCurrentAngle)) * arrowRadius;
        arrow.rectTransform.localRotation = Quaternion.Euler(0, 0, arrowCurrentAngle);
    }

    public void HideArrow()
    {
        arrow.gameObject.SetActive(false);
    }

    public void ShowMenu(string text, bool canContinute = false)
    {
        if (menuPanel.activeSelf) return;

        menuText.text = text;
        if (canContinute) menuContinuteButton.SetActive(true);
        else menuContinuteButton.SetActive(false);
        menuPanel.SetActive(true);
    }
    public void HideMenu()
    {
        menuPanel.SetActive(false);
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
    public void SetObjective(string text)
    {
        objectiveText.text = text;
    }
    public void ShowFloatingPanel(string text, Vector2 position)
    {
        floatingPanel.Show("<color=\"white\">Press <color=\"yellow\">E<color=\"white\"> to equip <color=\"yellow\">" + text);

        floatingPanelPosition.position = position;
    }

    private Color HSVInterp(Color c1, Color c2, float val)
    {
        float h, s, v;
        Color.RGBToHSV(c1, out h, out s, out v);
        var vec1 = new Vector3(h, s, v);
        Color.RGBToHSV(c2, out h, out s, out v);
        var vec2 = new Vector3(h, s, v);

        var interp = vec2 + (vec1 - vec2) * val;

        return Color.HSVToRGB(interp.x, interp.y, interp.z);
    }
    public void UpdateInfoText(int health, int ammo, int magazineCap, int ammoTotal, string currentWeapon, string[] inventory)
    {
        debugText.text = "HEALTH: " + health + "/100 \n"
            + "AMMO: " + ammo + "/" + ammoTotal + "\n"
            + "WEAPON: " + currentWeapon + "\n"
            + "INVENTORY: " + string.Join(",", inventory);

        float val = Math.Clamp(health / 100.0f, 0f, 1f);
        healthBar.fillAmount = val;
        healthText.text = health.ToString();
        healthBar.color = HSVInterp(Color.green * 0.5f, Color.red * 0.5f, val);

        if (magazineCap == 0)
        {
            ammoBar.fillAmount = 1;
            ammoText.text = "1/1";
        }
        else
        {
            val = Math.Clamp(ammo / (float)magazineCap, 0f, 1f);
            ammoBar.fillAmount = val;
            ammoBar.color = HSVInterp(Color.yellow * 0.9f, Color.red * 0.9f, val);
            ammoText.text = ammo + "/" + ammoTotal;
        }
    }
}
