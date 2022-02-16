
using UnityEngine;
using System;

public class KeyboardController : MonoBehaviour
{
    [SerializeField] private BaseCharacter _character;
    private float _moveTime = 0, _moveCooldown = 0.2f;

    private void LateUpdate()
    {
        var direction = new Vector3(Input.GetAxis(Axis.Horizontal), 0, Input.GetAxis(Axis.Vertical));
        MovementState movementState = MovementState.Idle;
        if (direction != Vector3.zero)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                movementState = _character.IsMoveBackwards(direction) ? MovementState.JumpBackwards : MovementState.Jump;
            }
            else if (Input.GetButton("Fire3"))
            {
                movementState = _character.IsMoveBackwards(direction) ? MovementState.RunBackwards : MovementState.Run;
            }
            else
            {
                movementState = _character.IsMoveBackwards(direction) ? MovementState.WalkBackwards : MovementState.Walk;
            }
            _character.MovementState = movementState;
            _character.MoveDirection = direction;
            _moveTime = _moveCooldown;
        }
    }

    private void FixedUpdate()
    {
        _moveTime -= Time.fixedDeltaTime;
        if (_moveTime <= 0)
        {
            _character.MovementState = MovementState.Idle;
            _character.MoveDirection = Vector3.zero;
        }
    }
}
