using System;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _playerCamera;
    [SerializeField] private BaseCharacter _character;
    [SerializeField] private float _panSpeed = 5f;
    [SerializeField] private Vector3 _positionOffset = new Vector3(0, 8, -5);
    [SerializeField] private Vector3 _rotationOffset = new Vector3(60f, 0f, 0f);

    private void FixedUpdate()
    {
        _playerCamera.transform.SetPositionAndRotation(Vector3.Lerp(_playerCamera.transform.position, _character.transform.position + _positionOffset, _panSpeed * Time.deltaTime), Quaternion.Euler(_rotationOffset));
    }
}
