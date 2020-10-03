using m039.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class SimpleOrbit : MonoBehaviour
{
    [Header("Settings")]
    public float radius = 10f;

    public float lineWidth = 2f;

    public Color lineColor = Color.green.WithAlpha(0.4f);

    public int lineSegments = 32;

    [Header("Dependencies")]
    public LineRenderer lineRenderer;

    void OnEnable()
    {
        UpdateLineRenderer();
    }

    void OnValidate()
    {
        UpdateLineRenderer();
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
