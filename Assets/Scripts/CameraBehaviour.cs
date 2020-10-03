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

    public float moveSpeed = 2;

    public float lookAtSpeed = 2f;

    Vector3 _newPosition;

    Vector3 _lookAtPosition;

    private void OnEnable()
    {
        UpdateCameraWithOffset();

        if (targetToLock != null)
        {
            _lookAtPosition = targetToLock.transform.position;
        }
    }

    private void OnValidate()
    {
        UpdateCameraWithOffset();
    }

    void LateUpdate()
    {
        UpdateCameraWithOffset();

        if (!Application.isPlaying)
            return;

        // Move to new position.
        {
            var delta = moveSpeed * Time.deltaTime;
            var direction = (_newPosition - Camera.main.transform.position);
            if (direction.magnitude > delta)
            {
                direction = direction.normalized * delta;
            }

            Camera.main.transform.position += direction;
        }

        // Lock to target.

        if (targetToLock != null)
        {
            var delta = lookAtSpeed * Time.deltaTime;
            var direction = (targetToLock.position - _lookAtPosition);
            if (direction.magnitude > delta)
            {
                direction = direction.normalized * delta;
            }

            _lookAtPosition += direction;

            Camera.main.transform.LookAt(_lookAtPosition);
        }
    }

    void UpdateCameraWithOffset()
    {
        if (player == null)
            return;

        var rotation = Quaternion.Euler(0, player.BulletRotation.eulerAngles.y, 0);
        var newPosition = player.transform.position + rotation * offset;

        if (Application.isPlaying)
        {
            _newPosition = newPosition;
        } else
        {
            transform.position = newPosition;
        }
    }
}
