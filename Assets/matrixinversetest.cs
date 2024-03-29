using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class matrixinversetest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // Definiere vier orthogonale 4x4-Matrizen
        Matrix4x4 testOne = new Matrix4x4(
            new Vector4(1, 0, 0, 0),
            new Vector4(0, 1, 0, 0),
            new Vector4(0, 0, 1, 0),
            new Vector4(0, 0, 0, 1)
        );

        Matrix4x4 testTwo = new Matrix4x4(
            new Vector4(1, 0, 0, 0),
            new Vector4(0, 0, 1, 0),
            new Vector4(0, -1, 0, 0),
            new Vector4(0, 0, 0, 1)
        );

        Matrix4x4 testThree = new Matrix4x4(
            new Vector4(0.6f, 0.8f, 0, 0),
            new Vector4(-0.8f, 0.6f, 0, 0),
            new Vector4(0, 0, 1, 0),
            new Vector4(0, 0, 0, 1)
        );

        Matrix4x4 testFour = new Matrix4x4(
            new Vector4(0.707f, -0.707f, 0, 0),
            new Vector4(0.707f, 0.707f, 0, 0),
            new Vector4(0, 0, 1, 0),
            new Vector4(0, 0, 0, 1)
        );

        Matrix4x4 inversedMyFunctionOne = MathTest.OrthogonalMatrixInverse(testOne); 
        if(inversedMyFunctionOne == testOne.inverse) 
        {
            Debug.Log("Test 1: bestanden!"); 
        } else 
        {
            Debug.Log("Test 1: f!");
        }


        Matrix4x4 inversedMyFunctionTwo = MathTest.OrthogonalMatrixInverse(testTwo);
        if (inversedMyFunctionTwo == testTwo.inverse)
        {
            Debug.Log("Test 2: bestanden!");
        }
        else
        {
            Debug.Log("Test 2: f!");
        }

        Matrix4x4 inversedMyFunctionThree = MathTest.OrthogonalMatrixInverse(testThree);
        if (inversedMyFunctionThree == testThree.inverse)
        {
            Debug.Log("Test 3 bestanden!");
        }
        else
        {
            Debug.Log("Test 3: f!");
        }

        Matrix4x4 inversedMyFunctionFour = MathTest.OrthogonalMatrixInverse(testFour);
        if (inversedMyFunctionFour == testFour.inverse)
        {
            Debug.Log("Test 4: bestanden!");
        }
        else
        {
            Debug.Log("Test 4: f!");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
