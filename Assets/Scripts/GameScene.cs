using m039.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GameScene : SingletonMonoBehaviour<GameScene>
{
    const float DefaultMotionBlurIntensity = 0f;

    const float DefaultLensDistortionIntensity = 0f;

    [Header("Dependencies")]
    public AudioSource audioSource;

    public PlayerBehaviour player;

    public CameraBehaviour camera;

    public BaseOrbit[] orbits;

    public Volume globalVolume;

    public float boostMotionBlurInensity = 0f;

    public float boostLensDistortionIntensity = 0f;

    public int startOrbitIndex = 0;

    public BaseOrbit CurrentOrbit { get; set; }

    MotionBlur _motionBlurTmp;

    LensDistortion _lensDistortionTmp;

    public bool UseBoostSpeed
    {
        get
        {
            return camera.UseBoostSpeed;
        }

        set
        {
            camera.UseBoostSpeed = value;

            if (globalVolume.profile.TryGet(out _motionBlurTmp))
            {
                _motionBlurTmp.intensity.value = value ? boostMotionBlurInensity : DefaultMotionBlurIntensity;
            }

            if (globalVolume.profile.TryGet(out _lensDistortionTmp))
            {
                _lensDistortionTmp.intensity.value = value ? boostLensDistortionIntensity : DefaultLensDistortionIntensity;
            }
        }
    }

    void Start()
    {
        // Set default states to orbits.

        orbits.ForEach((o) => o.HideFromPlayer());

        CurrentOrbit = orbits[startOrbitIndex];

        CurrentOrbit.ShowToPlayer(false);

        if (CurrentOrbit.IsEmpty)
        {
            ShowNextOrbit();
        }

        player.TransferToNewOrbit(CurrentOrbit, true);
        camera.ResetCamera();

        UseBoostSpeed = false;
    }

    public void ShowNextOrbit()
    {
        var index = System.Array.IndexOf(orbits, CurrentOrbit);
        if (index != -1 && index + 1 < orbits.Length)
        {
            var orbit = orbits[index + 1];

            if (!orbit.VisibileToPlayer) { 
                orbit.ShowToPlayer(true);
            }
        }
    }

    public void SelectNextOrbit()
    {
        var index = System.Array.IndexOf(orbits, CurrentOrbit);
        if (index != -1 && index + 1 < orbits.Length)
        {
            CurrentOrbit = orbits[index + 1];
        }

        CurrentOrbit.ShowToPlayer(false);
        player.TransferToNewOrbit(CurrentOrbit, false);
    }

    public void Play(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    protected override bool ShouldCreateIfNotExist => false;
}
