using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{

    [SerializeField] float mouseSensitivity = 1.0f;
    [SerializeField] Transform playerEntity;

    // Our respective vertical and horizontal viewangles in degrees
    private float lookPitch = 0, lookYaw = 0;

    // the above angles converted to quaternion
    private Quaternion lookQuat = Quaternion.identity;

    // and then converted to Vector3
    private Vector3 lookDirection = Vector3.forward;

    private InputHandler inputHandler;

    public void SetViewAngles(float yaw, float pitch)
    {
        lookYaw = yaw;
        lookPitch = pitch;
    }

    public Vector3 GetViewAngles()
    {
        return new Vector3(lookPitch, lookYaw);
    }

    // Brings the angles into a standard range
    private void NormalizeViewAngles()
    {
        lookPitch = MathF.Min(89.0f, MathF.Max(lookPitch, -89.0f));
        while (lookYaw > 180.0f)
            lookYaw -= 360.0f;
        while (lookYaw < -180.0f)
            lookYaw += 360.0f;
    }

    void Start()
    {
        GameManager.Instance.CursorLock();

        inputHandler = GameManager.Instance.InputHandler;
    }
    void LateUpdate()
    {
        lookYaw += inputHandler.MouseX * mouseSensitivity * Time.timeScale;
        lookPitch -= inputHandler.MouseY * mouseSensitivity * Time.timeScale;
        NormalizeViewAngles();

        lookQuat = Quaternion.Euler(lookPitch, lookYaw, 0);

        lookDirection = lookQuat * Vector3.forward;

        transform.rotation = lookQuat;
        if (playerEntity)
        {
            playerEntity.rotation = Quaternion.Euler(0, lookYaw, 0);
            transform.position = playerEntity.position;
        }
    }
}
