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
