using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TorchBehavior : MonoBehaviour
{
    [SerializeField] float rotateSpeed = 10.0f;

    void LateUpdate()
    {
        var camera = Camera.main.transform;


        transform.position = camera.position;
        transform.rotation = Quaternion.Lerp(transform.rotation, camera.rotation, Time.deltaTime * rotateSpeed);
    }
}
