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
    public bool Escape { get; private set; }
    public bool Run { get; private set; }

    private bool actualJump = false; public bool Jump
    {
        get { bool _old = actualJump; actualJump = false; return _old; }
        private set { if (value) actualJump = value; }
    }

    public bool Crouch { get; private set; }
    void Update()
    {
        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");

        Move = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        PrimaryAttack = Input.GetButton("Fire1");
        SecondaryAttack = Input.GetButtonDown("Fire2");
        UsePrimary = Input.GetKeyDown(KeyCode.E);
        Reload = Input.GetKeyDown(KeyCode.R);
        Run = Input.GetKey(KeyCode.LeftShift);
        Jump = Input.GetButtonDown("Jump");
        Crouch = Input.GetKey(KeyCode.LeftControl);
        Escape = Input.GetKeyDown(KeyCode.Escape);
    }
}
