using m039.Common;
using System;
using UnityEngine;
using UnityEngine.Rendering;

public class Collectable : BaseOrbitObject
{

    [HideInInspector]
    public float EmissionValue = 0f;

    public Color EmissionColor = Color.green;

    float _previousEmissionValue = float.NaN;

    //[NonSerialized]
    //Renderer _renderer;

    protected override void Awake()
    {
        base.Awake();

        if (!Application.isPlaying)
            return;

        //_renderer = GetComponentInChildren<Renderer>();
        //_renderer.material = new Material(_renderer.material);
    }

    private void LateUpdate()
    {
        if (!Application.isPlaying)
            return;

        if (_previousEmissionValue != EmissionValue)
        {
            SetEmissionValue(EmissionValue);
            _previousEmissionValue = EmissionValue;
        }
    }

    void SetEmissionValue(float value)
    {
        //_renderer.material.SetColor("_EmissionColor", EmissionColor.WithValue(value));
    }

}
