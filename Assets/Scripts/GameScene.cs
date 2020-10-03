using m039.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : SingletonMonoBehaviour<GameScene>
{
    [Header("Dependencies")]
    public AudioSource audioSource;

    public PlayerBehaviour player;

    public BaseOrbit[] orbits;

    public int startOrbitIndex = 0;

    public BaseOrbit CurrentOrbit { get; set; }

    void Awake()
    {
        CurrentOrbit = orbits[startOrbitIndex];

        // Set default states to orbits.

        foreach (var orbit in orbits)
        {
            if (orbit == CurrentOrbit)
            {
                orbit.ShowToPlayer();
            } else
            {
                orbit.HideFromPlayer();
            }
        }
    }

    public void ShowNextOrbit()
    {
        var index = System.Array.IndexOf(orbits, CurrentOrbit);
        if (index != -1 && index + 1 < orbits.Length)
        {
            orbits[index + 1].ShowToPlayer();
        }
    }

    public void SelectNextOrbit()
    {
        var index = System.Array.IndexOf(orbits, CurrentOrbit);
        if (index != -1 && index + 1 < orbits.Length)
        {
            CurrentOrbit = orbits[index + 1];
        }

        player.TransferToNewOrbit(CurrentOrbit);
    }

    public void Play(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    protected override bool ShouldCreateIfNotExist => false;
}
