using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LaserCanon : MonoBehaviour
{
    public int _maxReflections = 10;
    public Color _laserColor = Color.red;
    public float _lastReflectionLaserDistance = 100f;

    // Update is called once per frame
    void Update()
    {
        TraceLaser(transform.position, transform.forward, _maxReflections);
    }

    private void TraceLaser(Vector3 pos, Vector3 dir, int remainingReflection)
    {
        if (Physics.Raycast(pos, dir, out RaycastHit hitInfo))
        {
            Debug.DrawLine(pos, hitInfo.point, _laserColor);
            TraceLaser(hitInfo.point, MathTest.GetReflectionVector(dir, hitInfo.normal), remainingReflection - 1);
        }
        else
            Debug.DrawLine(pos, pos + dir * _lastReflectionLaserDistance, _laserColor);
    }
}
