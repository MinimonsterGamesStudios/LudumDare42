using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingCamera : MonoBehaviour
{

    [SerializeField]
    private GameObject _followedObject;
    [SerializeField]
    private float _smoothTime;

    private Vector3 _velocity = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        Camera camera = Camera.main;

        Vector3 targetPosition = _followedObject.transform.position;

        Vector3 point = camera.WorldToViewportPoint(targetPosition);
        Vector3 delta = targetPosition - camera.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
        Vector3 destination = transform.position + delta;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref _velocity, _smoothTime);
    }
}
