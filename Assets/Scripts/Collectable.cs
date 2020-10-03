using UnityEngine;

[ExecuteInEditMode]
public class Collectable : MonoBehaviour
{
    [Header("Settings")]
    [Range(0, 1)]
    public float position;

    public AudioClip pickSound;

    [Header("Dependencies")]
    public SimpleOrbit orbit;

    void OnEnable()
    {
        UpdatePosition();
    }

    void OnValidate()
    {
        UpdatePosition();
    }

    void UpdatePosition()
    {
        if (orbit != null)
        {
            var angle = position * 2 * Mathf.PI * Mathf.Rad2Deg;

            transform.position = orbit.GetPositionAlognOrbit(angle);
        }
    }

}
