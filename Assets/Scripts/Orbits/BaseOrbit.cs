using m039.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class BaseOrbit : MonoBehaviour
{
    public float radius = 10f;

    public float offsetRadius = 2f;

    public float lineWidth = 2f;

    public Color lineColor = Color.green.WithAlpha(0.4f);

    public int lineSegments = 32;

    public LineRenderer lineRenderer;

    void OnEnable()
    {
        UpdateLineRenderer();
    }

    void OnValidate()
    {
        UpdateLineRenderer();
    }

    public Vector3 GetPositionAlognOrbit(float angle, float positionOffsetX, float positionOffsetY)
    {
        return GetPositionAlognOrbit(angle, new Vector2(positionOffsetX, positionOffsetY));
    }

    public Vector3 GetPositionAlognOrbit(float angle, Vector2 positionOffset)
    {
        positionOffset.x = Mathf.Clamp(positionOffset.x, -1, 1);
        positionOffset.y = Mathf.Clamp(positionOffset.y, -1, 1);

        if (positionOffset.magnitude > 1)
        {
            positionOffset.Normalize();
        }

        var direction = Quaternion.Euler(0, -angle, 0) * Vector3.forward;

        var offset = Vector3.zero;

        offset += positionOffset.x * direction * offsetRadius;
        offset += positionOffset.y * Vector3.up * offsetRadius;

        return transform.position + direction * radius + offset;
    }

    void UpdateLineRenderer()
    {
        if (lineRenderer != null)
        {
            lineRenderer.startColor = lineColor;
            lineRenderer.endColor = lineColor;

            lineRenderer.positionCount = lineSegments;

            lineRenderer.loop = true;

            lineRenderer.startWidth = lineWidth;
            lineRenderer.endWidth = lineWidth;

            for (int i = 0; i < lineSegments; i++)
            {
                var angle = i * 2 * Mathf.PI / lineSegments * Mathf.Rad2Deg;
                lineRenderer.SetPosition(i, Quaternion.Euler(0, angle, 0) * Vector3.forward * radius);
            }
        }
    }

    public void PickObject(PlayerBehaviour player, BaseOrbitObject orbitObject)
    {
        if (orbitObject == null)
            return;

        GameScene.Instance.Play(orbitObject.pickSound);

        OnObjectPicked(player, orbitObject);
    }

    public virtual void OnObjectPicked(PlayerBehaviour player, BaseOrbitObject orbitObject)
    {
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position + Vector3.forward * radius, offsetRadius);
    }
}
