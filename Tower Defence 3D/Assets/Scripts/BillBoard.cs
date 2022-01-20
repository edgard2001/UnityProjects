using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBoard : MonoBehaviour
{
    [SerializeField] private Transform _camera;

    // Start is called before the first frame update
    void Start()
    {
        if (_camera == null) _camera = Camera.main.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(transform.position - _camera.forward);
    }
}
