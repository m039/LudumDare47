﻿using m039.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class PlayerBehaviour : MonoBehaviour
{
    const bool DigitalAxisSnap = false;

    const float DigitalAxisGravity = 3;

    const float DigitalAxisSensitivity = 5;

    [Header("Settings")]
    public float speed = 5f;

    [Header("Dependencies")]
    public SimpleOrbit orbit;

    public Transform bullet;

    float _angle = 0f;

    Vector3 _bulletStartRotation;

    public float OrbitAngle => _angle;

    DigitalAxisHelper _axisHelperX = new DigitalAxisHelper();

    DigitalAxisHelper _axisHelperY = new DigitalAxisHelper();

    void Awake()
    {
        _bulletStartRotation = bullet.rotation.eulerAngles;

        _axisHelperX.snap = () => DigitalAxisSnap;
        _axisHelperX.gravity = () => DigitalAxisGravity;
        _axisHelperX.sensitivity = () => DigitalAxisSensitivity;

        _axisHelperY.snap = () => DigitalAxisSnap;
        _axisHelperY.gravity = () => DigitalAxisGravity;
        _axisHelperY.sensitivity = () => DigitalAxisSensitivity;
    }

    void Update() {
        UpdatePositionOffset();
    }

    void UpdatePositionOffset()
    {
        _axisHelperX.SetValue(Input.GetAxisRaw("Horizontal"));
        _axisHelperY.SetValue(Input.GetAxisRaw("Vertical"));
    }

    void FixedUpdate()
    {
        UpdatePositionAndRotation();
    }

    void UpdatePositionAndRotation()
    {
        var delta = Time.deltaTime * speed;
        var deltaAngle = delta / (orbit.radius * 2) * Mathf.Rad2Deg;

        _angle += deltaAngle;

        if (_angle > 360)
        {
            _angle = 0;
        }
        else if (_angle < 0)
        {
            _angle += 360;
        }

        // Move player's center point.

        transform.position = orbit.GetPositionAlognOrbit(_angle, Vector2.zero);

        _axisHelperX.Update();
        _axisHelperY.Update();

        // Move and rotate the body of the player.

        bullet.position = orbit.GetPositionAlognOrbit(_angle, _axisHelperX.GetAxis(), _axisHelperY.GetAxis());
        bullet.rotation = Quaternion.Euler(_bulletStartRotation.x, _bulletStartRotation.y - _angle, _bulletStartRotation.z);
    }

    private void OnTriggerEnter(Collider collision)
    {
        orbit.PickObject(this, collision.gameObject.GetComponent<BaseOrbitObject>());
    }
}
