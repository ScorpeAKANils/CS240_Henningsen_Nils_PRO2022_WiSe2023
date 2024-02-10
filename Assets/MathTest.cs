using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MathTest
{
    public static float DotProduct(Vector3 a, Vector3 b)
    {
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

    public static Matrix4x4 OrthogonalMatrixInverse(Matrix4x4 A)
    {
        return GaußVerfahren(A);
    }

    private static Matrix4x4 GaußVerfahren(Matrix4x4 A)
    {
        int n = 4;
        float[,] initData = new float[4, 8];
        Matrix4x8 matrix = new Matrix4x8(initData);

        // Einfügen der Werte von A + Einheitsmatrix auf der rechten Seite
        for (int i = 0; i < n; i++)
        {
            for (int x = 0; x < 4; x++)
            {
                matrix.AddValues(i, x, A[i, x]);
            }

            matrix.AddValues(i, i + n, 1);
        }

        // Gauß-Jordan Elimination
        for (int i = 0; i < n; i++)
        {
            int pivotRow = i;

            for (int j = i + 1; j < n; j++)
            {
                if (Mathf.Abs(matrix.GetValue(j, i)) > Mathf.Abs(matrix.GetValue(pivotRow, i)))
                {
                    pivotRow = j;
                }
            }

            // Falls nötig, swap die Zeilen
            if (pivotRow != i)
            {
                for (int j = 0; j < 2 * n; j++)
                {
                    float temp = matrix.GetValue(i, j);
                    matrix.AddValues(i, j, matrix.GetValue(pivotRow, j));
                    matrix.AddValues(pivotRow, j, temp);
                }
            }

            // Pivot normalisieren
            float pivot = matrix.GetValue(i, i);
            for (int x = 0; x < 2 * n; x++)
            {
                float temp = matrix.GetValue(i, x);
                matrix.AddValues(i, x, temp / pivot);
            }

            for (int k = 0; k < n; k++)
            {
                if (k != i)
                {
                    float factor = matrix.GetValue(k, i);
                    for (int j = 0; j < 2 * n; j++)
                    {
                        float value = matrix.GetValue(k, j) - (factor * matrix.GetValue(i, j));
                        matrix.AddValues(k, j, value);
                    }
                }
            }
        }

        // Extrahiere die Inverse aus der erweiterten Matrix
        Matrix4x4 inversedMatrix = new Matrix4x4();
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                inversedMatrix[i, j] = matrix.GetValue(i, j + n);
            }
        }

        return inversedMatrix;
    }


    public static Vector3 MatrixPointMul(Matrix4x4 A, Vector3 p)
    {
        Vector3 result;

        result.x = A[0, 0] * p.x + A[0, 1] * p.y + A[0, 2] * p.z + A[0, 3];
        result.y = A[1, 0] * p.x + A[1, 1] * p.y + A[1, 2] * p.z + A[1, 3];
        result.z = A[2, 0] * p.x + A[2, 1] * p.y + A[2, 2] * p.z + A[2, 3];

        return result; 

    }

    public static Vector3 MatrixVectorMul(Matrix4x4 A, Vector3 v)
    {
        Vector3 result;

        result.x = A[0, 0] * v.x + A[0, 1] * v.y + A[0, 2] * v.z;
        result.y = A[1, 0] * v.x + A[1, 1] * v.y + A[1, 2] * v.z;
        result.z = A[2, 0] * v.x + A[2, 1] * v.y + A[2, 2] * v.z;

        return result;
    }

    public static Vector3 GetReflectionVector(Vector3 direction, Vector3 surfaceNormal)
    {
        return direction - 2 * DotProduct(direction, surfaceNormal) * surfaceNormal;
    }
    public static bool IsInsideCone(Vector2 pos, Vector2 coneOrigin, Vector2 viewDirection, float coneRadius, float coneHalfAngleDegree)
    {
        Vector2 distanceV = pos - coneOrigin;
        float dist = VectorLength(distanceV); // Berechne die Länge des Differenzvektors

        if (dist <= coneRadius)
        {
            Vector2 normalizedDistance = NormalizeVector(distanceV);
            Vector2 normalizedViewDirection = NormalizeVector(viewDirection);
            float dotProduct = DotProduct(normalizedDistance, normalizedViewDirection);
            float angleRad = Mathf.Acos(dotProduct);
            float angleDegree = angleRad * Mathf.Rad2Deg;

            if (angleDegree <= coneHalfAngleDegree)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        return false;
    }

    public static Vector3 MapPlayerMovementToWorldDirection(float _forwardPlayerInput, float _rightPlayerInput, Vector3 _cameraForward, Vector3 _cameraRight, Vector3 playerPosition, Vector3 planetOrigin)
    {
        Vector3 localMovementDirection = _rightPlayerInput * _cameraRight + _forwardPlayerInput * CrossProduct(_cameraRight, Vector3.up);
        localMovementDirection = NormalizeVector(localMovementDirection); 

        Vector3 playerOnSphere = playerPosition - planetOrigin;
        playerOnSphere = NormalizeVector(playerOnSphere);

        Vector3 projectedLocalMovementDirection = Vector3.ProjectOnPlane(localMovementDirection, playerOnSphere);
        projectedLocalMovementDirection = NormalizeVector(projectedLocalMovementDirection); 

        Vector3 worldMovementDirection = Quaternion.FromToRotation(Vector3.up, playerOnSphere) * projectedLocalMovementDirection;
        return worldMovementDirection;
    }

    public static Matrix4x4 MapWorldToMap(Matrix4x4 playerLocalToWorld, Matrix4x4 enemyLocalToWorld, Matrix4x4 mapLocalToWorld, float mapScaling)
    {
        throw new NotImplementedException();
    }

    public static Vector3 ProjectPointToPlane(Vector3 point, Vector3 planeOrigin, Vector3 planeNormal)
    {
        Vector3D projectedVector = new Vector3D(Vector3.zero);

        Vector3D distanceToOrigin = Vector3D.Subtract(point, planeOrigin);

        float distance = Vector3D.DotProduct(distanceToOrigin, planeNormal);
        Vector3D verschiebungsVector = Vector3D.VectorMulSkalar(planeNormal, distance);

        projectedVector = Vector3D.Subtract(point, verschiebungsVector);

        return Vector3D.ConvertToVector3(projectedVector); 

       //return projectedVector; 

    }

}

public struct Matrix4x8
{
    public float[,] data;

    public Matrix4x8(float[,] initData)
    {
        if (initData.GetLength(0) != 4 || initData.GetLength(1) != 8)
        {
            throw new ArgumentException("Input matrix must be 4x8. Erstellt ne 4x8 MAtrix und kriegt es nicht geschissen, eine als Parameter rein zu geben. GG, you played your self");
        }

        data = initData;
    }


    //zu faul nachzuschlagen wie man den Assignment Operator schreibt 
    public void AddValues(int i, int x, float value) 
    {
        if (i > 3 || i < 0 || x > 7 || x < 0)
        {
            throw new ArgumentException("Indexes where outside bounds, du lellek. Values: " + i + " and " + x + " Wenn die Werte allerdings 6 und 9 sind, dann vergiss das lellek, du macher");
        }
        data[i, x] = value;
    }

    public float GetValue(int i, int x) 
    {
        if (i > 3 || i < 0 || x > 7 || x<0)
        {
            throw new ArgumentException("Indexes where outside bounds, du lellek. Values: " + i + " and " + x + " Wenn die Werte allerdings 6 und 9 sind, dann vergiss das lellek, du macher");
        }
        return data[i, x];
    }
}

public struct Vector3D
{
    //werd mir das ding hier propably copy pasten um zu gucken wie weit ich damit in eigenen projecten kommen würde, deswegen gibts hier drin n haufen stuff
    public float x;
    public float y;
    public float z;

    //constructor for initialition with floats 
    public Vector3D(float X, float Y, float Z)
    {
        x = X;
        y = Y;
        z = Z;
    }
    //constructor with an Vector3 as init data 
    public Vector3D(Vector3 vectorToCopy)
    {
        x = vectorToCopy.x;
        y = vectorToCopy.y;
        z = vectorToCopy.z;
    }

    //Vector substraction + overloads
    public static Vector3D Subtract(Vector3D a, Vector3D b)
    {
        Vector3D result = new Vector3D(0, 0, 0);
        result.x = a.x - b.x;
        result.y = a.y - b.y;
        result.z = a.z - b.z;
        return result;
    }

    public static Vector3D Subtract(Vector3 a, Vector3D b)
    {
        Vector3D result = new Vector3D(0, 0, 0);
        result.x = a.x - b.x;
        result.y = a.y - b.y;
        result.z = a.z - b.z;
        return result;
    }

    public static Vector3D Subtract(Vector3 a, Vector3 b)
    {
        Vector3D result = new Vector3D(0, 0, 0);
        result.x = a.x - b.x;
        result.y = a.y - b.y;
        result.z = a.z - b.z;
        return result;
    }
    //vector additons + overloads
    public static Vector3D Addition(Vector3D a, Vector3D b) 
    {
        Vector3D result = new Vector3D(0, 0, 0);
        result.x = a.x + b.x;
        result.y = a.y + b.y;
        result.z = a.z + b.z;
        return result;
    }

    public static Vector3D Addition(Vector3 a, Vector3 b)
    {
        Vector3D result = new Vector3D(0, 0, 0);
        result.x = a.x + b.x;
        result.y = a.y + b.y;
        result.z = a.z + b.z;
        return result;
    }
    //dot products + overloads 
    public static float DotProduct(Vector3D a, Vector3D b)
    {
        return (a.x * b.x) + (a.y * b.y) + (a.z * b.z);
    }

    public static float DotProduct(Vector3D a, Vector3 b)
    {
        return (a.x * b.x) + (a.y * b.y) + (a.z * b.z);
    }

    public static float DotProduct(Vector3 a, Vector3 b)
    {
        return (a.x * b.x) + (a.y * b.y) + (a.z * b.z);
    }

    //multiplikation with an skalar + overloads 
    public static Vector3D VectorMulSkalar(Vector3D a, float skalar) 
    {
        return new Vector3D(a.x * skalar, a.y * skalar, a.z * skalar); 
    }


    public static Vector3D VectorMulSkalar(Vector3 a, float skalar)
    {
        return new Vector3D(a.x * skalar, a.y * skalar, a.z * skalar);
    }

    //Vector Convertions 
    public static Vector3 ConvertToVector3(Vector3D VectorToConvert) 
    {
        return new Vector3(VectorToConvert.x, VectorToConvert.y, VectorToConvert.z); 
    }

    public static Vector3D ConvertToVector3D(Vector3 VectorToConvert)
    {
        return new Vector3D(VectorToConvert.x, VectorToConvert.y, VectorToConvert.z);
    }
}


//nach nem semester in Unreal muss ich sagen, die entwicklung von C# ist ein segen, in C++ hätte ich nichtmal bock gehabt anzufangen lol 
