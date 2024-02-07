using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{
    public Transform _playerShip;
    public float _mapScale = 0.1f;
    private GameObject[] _enemies;
    public Mesh _indicatorMesh;
    public Material _indicatorMaterial;


    void Start()
    {
        _enemies = GameObject.FindGameObjectsWithTag("Enemy");
    }

    // Update is called once per frame
    void LateUpdate()
    {
        foreach(var eGO in _enemies)
        {
            var mapMatrix = MathTest.MapWorldToMap(_playerShip.localToWorldMatrix, eGO.transform.localToWorldMatrix, transform.localToWorldMatrix, _mapScale);

            Graphics.DrawMesh(_indicatorMesh, mapMatrix, _indicatorMaterial, 0);
            var shipMapPos = mapMatrix.MultiplyPoint(Vector3.zero);
            Debug.DrawLine(shipMapPos, MathTest.ProjectPointToPlane(shipMapPos, transform.position, transform.up),Color.red);
        }
    }
}
