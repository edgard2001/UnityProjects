using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundParallax : MonoBehaviour
{

    [SerializeField]
    private float _parallaxScaleX = 0.90f;
    [SerializeField]
    private float _parallaxScaleY = 0.99f;

    private Vector3 _previousCameraPosition;
    private Transform _camera;

    void Awake()
    {
        _camera = GameObject.Find("Player Camera").GetComponent<Transform>();
        _previousCameraPosition = _camera.position;

        _parallaxScaleX = Mathf.Clamp(_parallaxScaleX, 0f, 1f);
        _parallaxScaleY = Mathf.Clamp(_parallaxScaleY, 0f, 1f);
    }

    void LateUpdate()
    {
        Vector3 displacement = Vector3.zero;
        displacement.x = (_camera.position.x - _previousCameraPosition.x) * _parallaxScaleX;
        displacement.y = (_camera.position.y - _previousCameraPosition.y) * _parallaxScaleY;
        transform.position += displacement;
        _previousCameraPosition = _camera.position;
    }
}
