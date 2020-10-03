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

    private void OnEnable()
    {
        UpdateCameraWithOffset();
    }

    private void OnValidate()
    {
        UpdateCameraWithOffset();
    }

    void LateUpdate()
    {
        UpdateCameraWithOffset();

        if (targetToLock != null)
        {
            Camera.main.transform.LookAt(targetToLock);
        }
    }

    void UpdateCameraWithOffset()
    {
        if (player == null)
            return;

        var rotation = Quaternion.Euler(0, player.BulletRotation.eulerAngles.y, 0);

        transform.position = player.transform.position + rotation * offset;
    }
}
