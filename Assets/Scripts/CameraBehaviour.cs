using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraBehaviour : MonoBehaviour
{
    [Header("Settings")]
    public Transform targetToLock;

    [Header("Dependencies")]
    public PlayerBehaviour player;

    Vector3 _toOrbit;

    private void OnEnable()
    {
        if (Application.isPlaying)
        {
            _toOrbit = Camera.main.transform.position - player.orbit.transform.position;
        }
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
}
