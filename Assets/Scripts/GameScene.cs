using m039.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : SingletonMonoBehaviour<GameScene>
{
    [Header("Dependencies")]
    public AudioSource audioSource;

    public PlayerBehaviour player;

    public CameraBehaviour camera;

    public BaseOrbit[] orbits;

    public int startOrbitIndex = 0;

    public BaseOrbit CurrentOrbit { get; set; }

    public bool UseBoostSpeed
    {
        get {
            return camera.UseBoostSpeed;
        }

        set
        {
            camera.UseBoostSpeed = value;
        }
    }

    void Awake()
    {
        CurrentOrbit = orbits[startOrbitIndex];

        // Set default states to orbits.

        foreach (var orbit in orbits)
        {
            if (orbit == CurrentOrbit)
            {
                orbit.ShowToPlayer(false);
            } else
            {
                orbit.HideFromPlayer();
            }
        }

        player.TransferToNewOrbit(CurrentOrbit, true);
    }

    public void ShowNextOrbit()
    {
        var index = System.Array.IndexOf(orbits, CurrentOrbit);
        if (index != -1 && index + 1 < orbits.Length)
        {
            orbits[index + 1].ShowToPlayer(true);
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
