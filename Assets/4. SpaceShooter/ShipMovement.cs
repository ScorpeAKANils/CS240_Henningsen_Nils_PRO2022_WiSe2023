using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{

    public float _speed;
    public float _yawSpeed;
    public float _pitchSpeed;
    public float _rollSpeed;

    private float _yaw;
    private float _pitch;
    private float _roll;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = transform.position + transform.forward * Time.deltaTime * _speed;

        float yaw = Input.GetKey(KeyCode.D) ? 1 : 0;
        yaw -= Input.GetKey(KeyCode.A) ? 1 : 0;
        float pitch = Input.GetKey(KeyCode.W) ? 1 : 0;
        pitch -= Input.GetKey(KeyCode.S) ? 1 : 0;
        float roll = Input.GetKey(KeyCode.Q) ? 1 : 0;
        roll -= Input.GetKey(KeyCode.E) ? 1 : 0;

        yaw *= _yawSpeed * Time.deltaTime;
        pitch *= _pitchSpeed * Time.deltaTime;
        roll *= _rollSpeed * Time.deltaTime;

        _yaw += yaw;
        _pitch += pitch;
        _roll += roll;

        transform.Rotate(pitch, yaw, roll);


    }
}
