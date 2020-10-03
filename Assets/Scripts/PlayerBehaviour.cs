using m039.Common;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
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

    public Transform bullet;

    public Quaternion BulletRotation => bullet == null ? Quaternion.identity : bullet.rotation;

    float _angle = 0f;

    Vector3 _bulletStartRotation;

    public float OrbitAngle => _angle;

    DigitalAxisHelper _axisHelperX = new DigitalAxisHelper();

    DigitalAxisHelper _axisHelperY = new DigitalAxisHelper();

    bool _blockInput = false;

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
        if (!_blockInput)
        {
            _axisHelperX.SetValue(Input.GetAxisRaw("Horizontal"));
            _axisHelperY.SetValue(Input.GetAxisRaw("Vertical"));
        }
    }

    void FixedUpdate()
    {
        UpdatePositionAndRotation();
    }

    public void TransferToNewOrbit(BaseOrbit newOrbit)
    {
        // Transfer angle.

        var direction = (transform.position - newOrbit.transform.position)
            .normalized
            .WithY(0);

        _angle = Vector3.Angle(Vector3.forward, direction);

        // Reset input.

        ResetInputAxisHelpers();
    }

    public void ResetInputAxisHelpers()
    {
        _axisHelperX.SetAxis(0f);
        _axisHelperX.SetValue(0f);

        _axisHelperY.SetAxis(0f);
        _axisHelperY.SetValue(0f);

        IEnumerator blockInput()
        {
            _blockInput = true;
            yield return new WaitForSeconds(0.5f);
            _blockInput = false;
        }

        StartCoroutine(blockInput());
    }

    void UpdatePositionAndRotation()
    {
        var currentOrbit = GameScene.Instance.CurrentOrbit;
        var delta = Time.deltaTime * speed;
        var deltaAngle = delta / (currentOrbit.radius * 2) * Mathf.Rad2Deg;

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

        // Handle direction

        var angle = _angle;

        if (currentOrbit.orbitDirection == BaseOrbit.OrbitDirection.CW)
        {
            angle = 360 - _angle;
        }

        transform.position = currentOrbit.GetPositionAlognOrbit(angle, Vector2.zero);

        _axisHelperX.Update();
        _axisHelperY.Update();

        // Move and rotate the body of the player.

        var xAxis = _axisHelperX.GetAxis();
        var yAxis = _axisHelperY.GetAxis();

        if (currentOrbit.orbitDirection == BaseOrbit.OrbitDirection.CW)
        {
            xAxis = -1 * xAxis;
        }

        bullet.position = currentOrbit.GetPositionAlognOrbit(angle, xAxis, yAxis);

        if (currentOrbit.orbitDirection == BaseOrbit.OrbitDirection.CW)
        {
            angle += 180; // Rotate the bullet further.
        }

        bullet.rotation = Quaternion.Euler(_bulletStartRotation.x, _bulletStartRotation.y - angle, _bulletStartRotation.z);
    }

    private void OnTriggerEnter(Collider collision)
    {
        GameScene.Instance.CurrentOrbit.PickObject(this, collision.gameObject.GetComponent<BaseOrbitObject>());
    }
}
