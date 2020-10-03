using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class PlayerBehaviour : MonoBehaviour
{
    [Header("Settings")]
    public float speed = 5f;

    [Header("Dependencies")]
    public SimpleOrbit orbit;

    public Transform bullet;

    float _angle = 0f;

    Vector3 _bulletStartRotation;

    public float OrbitAngle => _angle;

    void Awake()
    {

        _bulletStartRotation = bullet.rotation.eulerAngles;
    }

    void Update()
    {
        var delta = Time.deltaTime * speed;
        var deltaAngle = delta / (orbit.radius * 2) * Mathf.Rad2Deg;

        _angle += deltaAngle;

        transform.position = orbit.transform.position + Quaternion.Euler(0, 180 - _angle, 0) * Vector3.forward * orbit.radius;

        if (_angle > 360)
        {
            _angle = 0;
        }
        else if (_angle < 0)
        {
            _angle += 360;
        }

        bullet.rotation = Quaternion.Euler(_bulletStartRotation.x, _bulletStartRotation.y - _angle, _bulletStartRotation.z);
    }
}
