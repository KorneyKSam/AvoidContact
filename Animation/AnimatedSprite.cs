using System;
using UnityEngine;

public class AnimatedSprite : MonoBehaviour
{
    private Animator _animator;

    private DirectionState _directionState;
    public DirectionState DirectionState
    {
        get { return _directionState; }
        set
        {
            transform.localScale = Directions.Values[_directionState];
            _directionState = value;
        }
    }

    private MovementState _state;
    public MovementState State
    {
        get { return _state; }
        set
        {
            _animator?.Play(value.ToString());
            _state = value;
        }
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        if (_animator == null)
            throw new NullReferenceException("AnimatedSprite doesn't have an Animator!");
    }
}
