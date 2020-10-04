using m039.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SimpleOrbit : BaseOrbit
{
    [Header("Simple Orbit")]

    public Collectable[] collectables;

    public Door door;

    public override bool IsEmpty => collectables.Length == 0;

    Spike[] _spikes; // Should it be more generic?

    void Awake()
    {
        if (Application.isPlaying)
        {
            door.SetVisibility(false);
        }

        _spikes = transform.GetComponentsInChildren<Spike>();
    }

    public override void OnObjectPicked(PlayerBehaviour player, BaseOrbitObject orbitObject)
    {
        base.OnObjectPicked(player, orbitObject);

        // If picked collectable

        if (orbitObject is Collectable)
        {
            orbitObject.SetVisibility(false);

            // Show door if needed

            if (collectables.All((c) => !c.GetVisibility()))
            {
                SetLineRenderWithCompletedColor();

                GameScene.Instance.ShowNextOrbit();
                if (door != null)
                {    
                    door.SetVisibility(true);
                }
            }
        }

        if (orbitObject is Spike)
        {
            GameScene.Instance.RestartFromLastCheckpoint();
        }

        // If door picked
        if (orbitObject is Door) {
            NextOrbit();
        }
    }

    void NextOrbit()
    {
        HideAll();
        GameScene.Instance.SelectNextOrbit();
    }

    public override void HideFromPlayer()
    {
        base.HideFromPlayer();
        HideAll();
    }

    public override void ShowToPlayer(bool inactive)
    {
        base.ShowToPlayer(inactive);
        Restart(inactive);
    }

    void HideAll()
    {
        collectables.ForEach((c) => c.SetVisibility(false));
        door.SetVisibility(false);
        SetVisibility(false);
    }

    void Restart(bool inactive)
    {
        SetVisibility(true);
        collectables.ForEach((c) => c.SetVisibility(true));

        if (collectables.Length == 0)
        {
            if (inactive)
            {
                SetLineRenderWithInactiveColor();
            } else
            {
                SetLineRenderWithCompletedColor();
            }

            GameScene.Instance.ShowNextOrbit();
            door.SetVisibility(true);
        } else
        {
            door.SetVisibility(false);
        }
    }

    public override void SetVisibility(bool visibility)
    {
        base.SetVisibility(visibility);

        _spikes.ForEach((c) => c.SetVisibility(visibility));
    }
}
