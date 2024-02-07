using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform _model;
    public Transform _camPivot;
    public Transform _cam;
    public Transform _planet;
    public float _speed = 4f;
    public float _camRotationSpeed = 1f;

    private float _camYawRotation = 0f;
    private float _camPitchRotation = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        UpdatePlayerMovement();
        UpdateCameraMovement();

    }

    private void UpdatePlayerMovement()
    {
        float playerForwardMovement = Input.GetKey(KeyCode.W) ? 1 : 0;
        playerForwardMovement -= Input.GetKey(KeyCode.S) ? 1 : 0;
        float rightMovement = Input.GetKey(KeyCode.D) ? 1 : 0;
        rightMovement -= Input.GetKey(KeyCode.A) ? 1 : 0;
        var movementDir = MathTest.MapPlayerMovementToWorldDirection(playerForwardMovement, rightMovement, _cam.forward, _cam.right, transform.position, _planet.position);
        if(movementDir.magnitude > 0.1f)
            _model.rotation =  Quaternion.LookRotation(movementDir, (transform.position - _planet.position).normalized);
        transform.position = Time.deltaTime * _speed * movementDir + transform.position;
    }

    private void UpdateCameraMovement()
    {
        float yawRotation = Input.GetKey(KeyCode.RightArrow) ? 1 : 0;
        yawRotation -= Input.GetKey(KeyCode.LeftArrow) ? 1 : 0;
        float pitchRotation = Input.GetKey(KeyCode.UpArrow) ? 1 : 0;
        pitchRotation -= Input.GetKey(KeyCode.DownArrow) ? 1 : 0;
        _camYawRotation += yawRotation * Time.deltaTime * _camRotationSpeed;
        _camPitchRotation += pitchRotation * Time.deltaTime * _camRotationSpeed;
        _camPitchRotation = Mathf.Clamp(_camPitchRotation, -70f, 70f);

        _camPivot.localRotation = Quaternion.Euler(_camPitchRotation, _camYawRotation, 0);

    }

    private void LateUpdate()
    {
        var newUp = (transform.position - _planet.position).normalized;
        transform.position = newUp * _planet.localScale.x * 0.5f + _planet.position;
        transform.rotation = Quaternion.FromToRotation(transform.up, newUp) * transform.rotation;
    }
}
