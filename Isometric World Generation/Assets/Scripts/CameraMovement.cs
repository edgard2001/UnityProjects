using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private float _movementScreenMargin = 0.1f;
    [SerializeField] private float _moveSpeed = 2f;

    private Camera _camera;
    private Transform _cameraTransform;
    private Vector3 _input;


    // Start is called before the first frame update
    void Start()
    {
        _camera = gameObject.GetComponent<Camera>();
        _cameraTransform = _camera.transform;
    }

    // Update is called once per frame
    void Update()
    {
        _input = _camera.ScreenToViewportPoint(Input.mousePosition);

        if (_input.x > 1 - _movementScreenMargin) _input.x = _moveSpeed;
        else if (_input.x < _movementScreenMargin) _input.x = -_moveSpeed;
        else _input.x = 0;

        if (_input.y > 1 - _movementScreenMargin) _input.y = _moveSpeed;
        else if (_input.y < _movementScreenMargin) _input.y = -_moveSpeed;
        else _input.y = 0;

        _cameraTransform.position += _input * _camera.orthographicSize * Time.smoothDeltaTime;

        _camera.orthographicSize += Input.mouseScrollDelta.y * 100f * Time.smoothDeltaTime;
        _camera.orthographicSize = Mathf.Clamp(_camera.orthographicSize, 1f, 20f);
    }
}
