using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Agent : MonoBehaviour
{
    public int _visHalfAngle = 45;
    public float _visRadius = 10f;
    public Color _donSeeColor = Color.blue;
    public Color _seeColor = Color.red;
    public Transform _target;

    private void Update()
    {
        drawVisCone();
        
    }

    private void drawVisCone()
    {

        Color col = MathTest.IsInsideCone(_target.position, transform.position, transform.right, _visRadius, _visHalfAngle) ? _seeColor : _donSeeColor;

        for (int a = -_visHalfAngle; a < _visHalfAngle;a++)
        {
            Vector3 from = GetSamplePos(a);
            Vector3 to = GetSamplePos(a + 1);
            Debug.DrawLine(from, to, col);
        }

        Debug.DrawLine(transform.position, GetSamplePos(-_visHalfAngle), col);
        Debug.DrawLine(transform.position, GetSamplePos(_visHalfAngle), col);

    }

    private Vector3 GetSamplePos(int angleInDegree)
    {
        return transform.position + Quaternion.Euler(0, 0, angleInDegree) * transform.right * _visRadius;
    }
}
