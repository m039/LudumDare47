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

    void Awake()
    {
        if (Application.isPlaying)
        {
            door.SetVisibility(false);
        }
    }

    public override void OnObjectPicked(PlayerBehaviour player, BaseOrbitObject orbitObject)
    {
        // If picked collectable

        if (orbitObject is Collectable)
        {
            orbitObject.SetVisibility(false);

            // Show door if needed

            if (collectables.All((c) => !c.GetVisibility()))
            {
                GameScene.Instance.ShowNextOrbit();
                if (door != null)
                {
                    door.SetVisibility(true);
                }
            }
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
        Restart();
    }

    void HideAll()
    {
        collectables.ForEach((c) => c.SetVisibility(false));
        door.SetVisibility(false);
        SetVisibility(false);
    }

    void Restart()
    {
        SetVisibility(true);
        collectables.ForEach((c) => c.SetVisibility(true));
        if (door != null)
        {
            door.SetVisibility(false);
        }
    }
}
