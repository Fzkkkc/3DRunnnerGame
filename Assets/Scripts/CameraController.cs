using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _player;
    private Vector3 _offset;

    void Start()
    {
        _offset = transform.position - _player.position;
    }

    void FixedUpdate()
    {
        Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, _offset.z + _player.position.z);
        transform.position = newPosition;
    }
}
