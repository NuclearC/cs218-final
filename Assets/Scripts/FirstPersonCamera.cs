using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{

    [SerializeField] float mouseSensitivity = 1.0f;
    [SerializeField] Transform playerEntity;

    // Our respective vertical and horizontal viewangles in degrees
    private float lookPitch = 0, lookYaw = 0;
    private float recoilPitch = 0, recoilYaw = 0;

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

    public Vector3 GetViewDirection()
    {
        return lookDirection;
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

    public void AddRecoil(float dPitch, float dYaw)
    {
        recoilYaw += dYaw;
        recoilPitch += dPitch;
    }

    float SlowAdd(out float o, float a, float b, float c)
    {
        float d = a * c;
        o = a - d;
        return b + d;
    }

    void LateUpdate()
    {
        lookYaw += inputHandler.MouseX * mouseSensitivity;
        lookPitch -= inputHandler.MouseY * mouseSensitivity;

        lookYaw = SlowAdd(out recoilYaw, recoilYaw, lookYaw, Time.deltaTime * 18.0f);
        lookPitch = SlowAdd(out recoilPitch, recoilPitch, lookPitch, Time.deltaTime * 18.0f);

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
