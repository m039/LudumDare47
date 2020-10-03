using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CameraBehaviour : MonoBehaviour
{
    public Transform targetToLock;

    void Update()
    {
        if (targetToLock != null)
        {
            Camera.main.transform.LookAt(targetToLock);
        }
    }
}
