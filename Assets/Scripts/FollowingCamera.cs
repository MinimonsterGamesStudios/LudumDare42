using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{

    [SerializeField]
    private GameObject _followedObject;

    [SerializeField]
    private float _smoothTime;

    [SerializeField]
    private Vector3 _offset;

    private Vector3 _velocity = Vector3.zero;

    void FixedUpdate()
    {
        Vector3 desiredPosition = _followedObject.transform.position + _offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, _smoothTime);
        transform.position = smoothedPosition;

       // transform.LookAt(_followedObject.transform);
    }
}

