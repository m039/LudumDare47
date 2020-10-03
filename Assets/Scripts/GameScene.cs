using m039.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScene : SingletonMonoBehaviour<GameScene>
{
    [Header("Dependencies")]
    public AudioSource audioSource;

    public void Play(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }

    protected override bool ShouldCreateIfNotExist => false;
}
