using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LavaFollow : MonoBehaviour
{
    [SerializeField]
    private Transform _player;
    [SerializeField]
    private Vector3 _offset;
    [SerializeField]
    private Vector3 _constantPosition;

    // Update is called once per frame
    void Update()
    {
        transform.position = (new Vector3(0, _player.position.y, 0) + _offset) + _constantPosition;

    }
}
