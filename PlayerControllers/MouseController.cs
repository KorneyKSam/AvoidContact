using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MouseController : MonoBehaviour
{
    [SerializeField] private UnityEvent<Vector3> MousePositionEvent;
    [SerializeField] private BaseCharacter _baseCharacter;
    [SerializeField] private Camera _playerCamera;

    private Vector3 _mousePosition;

    public MouseController(Camera playerCamera)
    {
        _playerCamera = playerCamera;
    }
    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _baseCharacter.Shoot();
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            _baseCharacter.DropWeapon();
        }
    }

    public void FixedUpdate()
    {
        Vector3 newMousePosition = _mousePosition;
        Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            newMousePosition = new Vector3(raycastHit.point.x, transform.position.y, raycastHit.point.z);
        }
        if (_mousePosition != newMousePosition)
        {
            _mousePosition = newMousePosition;
            MousePositionEvent.Invoke(newMousePosition);
        }
    }
}
