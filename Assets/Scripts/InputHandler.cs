using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{

    public float MouseX { get; private set; }
    public float MouseY { get; private set; }
    public Vector2? Move { get; private set; }

    public bool PrimaryAttack { get; private set; }
    public bool SecondaryAttack { get; private set; }
    public bool UsePrimary { get; private set; }
    public bool Reload { get; private set; }

    void Update()
    {
        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");

        Move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        PrimaryAttack = Input.GetButton("Fire1");
        SecondaryAttack = Input.GetButtonDown("Fire2");
        UsePrimary = Input.GetKeyDown(KeyCode.E);
        Reload = Input.GetKeyDown(KeyCode.R);
    }
}
