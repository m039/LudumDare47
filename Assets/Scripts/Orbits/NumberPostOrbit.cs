using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using m039.Common;
using System.Linq;

public class NumberPostOrbit : BaseOrbit
{
    // 1 4 2 3
    public NumberPost[] numberPosts;

    public Door door;

    public override bool IsEmpty => numberPosts.Length == 0;

    public AudioClip wrongSequenceSound;

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

        if (orbitObject is NumberPost)
        {
            orbitObject.SetVisibility(false);

            // Show door if needed

            if (numberPosts.All((c) => !c.GetVisibility()))
            {
                SetLineRenderWithCompletedColor();

                GameScene.Instance.ShowNextOrbit();
                if (door != null)
                {
                    door.SetVisibility(true);
                }
            }
        }

        // If door picked
        if (orbitObject is Door)
        {
            HideFromPlayer();
            GameScene.Instance.SelectNextOrbit();
        }
    }

    public override void HideFromPlayer()
    {
        base.HideFromPlayer();

        numberPosts.ForEach((c) => c.SetVisibility(false));
        door.SetVisibility(false);
        SetVisibility(false);
    }

    public override void ShowToPlayer(bool inactive)
    {
        base.ShowToPlayer(inactive);

        SetVisibility(true);
        numberPosts.ForEach((c) => c.SetVisibility(true));

        if (numberPosts.Length == 0)
        {
            if (inactive)
            {
                SetLineRenderWithInactiveColor();
            }
            else
            {
                SetLineRenderWithCompletedColor();
            }

            GameScene.Instance.ShowNextOrbit();
            door.SetVisibility(true);
        }
        else
        {
            door.SetVisibility(false);
        }
    }
}
