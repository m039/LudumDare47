﻿using UnityEngine;

[ExecuteInEditMode]
public class Collectable : MonoBehaviour
{
    [Header("Settings")]
    [Range(0, 1)]
    public float position;

    [Range(0, 1)]
    public float positionOffsetMagnitude;

    [Range(0, 360)]
    public float positionOffsetAngle;

    public AudioClip pickSound;

    [Header("Dependencies")]
    public SimpleOrbit orbit;

    void OnEnable()
    {
        UpdatePosition();
    }

    void OnValidate()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        if (orbit != null)
        {
            var angle = position * 2 * Mathf.PI * Mathf.Rad2Deg;

            var positionOffset = Vector2.zero;

            positionOffset.y = Mathf.Sin(positionOffsetAngle * Mathf.Deg2Rad);
            positionOffset.x = Mathf.Cos(positionOffsetAngle * Mathf.Deg2Rad);

            positionOffset *= positionOffsetMagnitude;

            transform.position = orbit.GetPositionAlognOrbit(angle, positionOffset);
        }
    }

}
