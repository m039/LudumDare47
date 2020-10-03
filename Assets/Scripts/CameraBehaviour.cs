using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraBehaviour : MonoBehaviour
{
    [Header("Settings")]
    public Transform targetToLock;

    public Vector3 offset = Vector3.one;

    [Header("Dependencies")]
    public PlayerBehaviour player;

    Vector3 _toOrbit;

    private void OnEnable()
    {
        UpdateCameraWithOffset();
        if (Application.isPlaying)
        {
            _toOrbit = Camera.main.transform.position - player.orbit.transform.position;
        }
    }

    private void OnValidate()
    {
        UpdateCameraWithOffset();
    }

    void LateUpdate()
    {
        if (Application.isPlaying)
        {
            if (player != null)
            {
                Camera.main.transform.position = player.orbit.transform.position + Quaternion.Euler(0, -player.OrbitAngle, 0) * _toOrbit;
            }
        }

        if (targetToLock != null)
        {
            Camera.main.transform.LookAt(targetToLock);
        }
    }

    void UpdateCameraWithOffset()
    {
        if (player == null)
            return;

        transform.position = player.transform.position + offset;
    }
}
