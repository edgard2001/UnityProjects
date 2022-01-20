using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaseInTargetFollower : ITargetFollower
{
    private float _maxDistanceSquared;
    private Transform _source;
    private Transform _target;
    private Vector3 _offset;

    public EaseInTargetFollower(Transform source, Transform target, Vector3 offset, float maxDistance)
    {
        _source = source;
        _target = target;
        _offset = offset;
        _maxDistanceSquared = maxDistance * maxDistance;
    }

    public void MoveToNextPosition()
    {
        float distanceSquared = (_target.position + _offset - _source.position).sqrMagnitude;
        float time = Mathf.Clamp(distanceSquared / _maxDistanceSquared, 0f, 1f);
        _source.position = Vector3.Lerp(_source.position, _target.position + _offset, time);
    }
}
