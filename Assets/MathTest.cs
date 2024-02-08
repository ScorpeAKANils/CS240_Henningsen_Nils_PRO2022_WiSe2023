using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathTest
{
    public static float DotProduct(Vector3 a, Vector3 b)
    {
        //return Vector3.Dot(a, b);
        return (a.x * b.x) + (a.y * b.y) + (a.z * b.z);
    }

    public static Vector3 CrossProduct(Vector3 a, Vector3 b)
    {
        Vector3 result = new Vector3(0, 0, 0);

        result.x = (a.y * b.z) - (a.z * b.y);
        result.y = (a.z * b.x) - (a.x * b.z);
        result.z = (a.x * b.y) - (a.y * b.x);

        return result;
    }

    public static float VectorLength(Vector3 a)
    {
        return MathF.Sqrt(MathF.Pow(a.x, 2) + MathF.Pow(a.y, 2) + MathF.Pow(a.z, 2));
    }

    public static Vector3 NormalizeVector(Vector3 a)
    { 

        float lenght = VectorLength(a); 
        return a / lenght;
    }

    public static Matrix4x4 MatrixMultiplikation(Matrix4x4 A, Matrix4x4 B)
    {
        Matrix4x4 result = Matrix4x4.zero; 
        for(int i = 0; i <= 3; i++) 
        {
            for(int j = 0; j <= 3; j++) 
            {
                float sum = 0; 
                for(int k = 0; k <= 3; k++) 
                {
                    sum += A[i, k] * B[k, j]; 
                }
                result[i, j] = sum;
            }
        }
        return result;
    }

    // This function assumes an orthogonal Matrix A
    public static Matrix4x4 OrthogonalMatrixInverse(Matrix4x4 A)
    {
        //Schritt 1: Determinante bestimmen
        //Schritt 2: ....
        //Schritt 3: Profit 
        return A.inverse;
    }

    public static Vector3 MatrixPointMul(Matrix4x4 A, Vector3 p)
    {
        return A.MultiplyPoint(p);
    }

    public static Vector3 MatrixVectorMul(Matrix4x4 A, Vector3 v)
    {
        return A.MultiplyVector(v);
    }

    public static Vector3 GetReflectionVector(Vector3 direction, Vector3 surfaceNormal)
    {
        throw new NotImplementedException();
    }


    public static bool IsInsideCone(Vector2 pos, Vector2 coneOrigin, Vector2 viewDirection, float coneRadius, float coneHalfAngleDegree)
    {
        throw new NotImplementedException();
    }

    public static Vector3 MapPlayerMovementToWorldDirection(float _forwardPlayerInput, float _rightPlayerInput, Vector3 _cameraForward, Vector3 _cameraRight, Vector3 playerPosition, Vector3 planetOrigin)
    {
        throw new NotImplementedException();
    }

    public static Matrix4x4 MapWorldToMap(Matrix4x4 playerLocalToWorld, Matrix4x4 enemyLocalToWorld, Matrix4x4 mapLocalToWorld, float mapScaling)
    {
        throw new NotImplementedException();
    }

    public static Vector3 ProjectPointToPlane(Vector3 point, Vector3 planeOrigin, Vector3 planeNormal)
    {
        throw new NotImplementedException();

    }

}
