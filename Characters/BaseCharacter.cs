using AvoidContact.CharacterStats;
using System;
using UnityEngine;

public abstract class BaseCharacter : MonoBehaviour
{
    [Header("Base Stats")]
    [SerializeField] protected CharacterStat health;
    [SerializeField] protected CharacterStat armor;
    [SerializeField] protected CharacterStat shield;
    [SerializeField] protected CharacterStat saintiy;

    [Header("Movement Stats")]
    [SerializeField] protected CharacterStat walkSpeed;
    [SerializeField] protected CharacterStat walkBackwardsSpeed;
    [SerializeField] protected CharacterStat runSpeed;
    [SerializeField] protected CharacterStat runBackwardsSpeed;
    [SerializeField] protected CharacterStat jumpForce;
    [SerializeField] protected CharacterStat jumpBacwardsForce;

    [Header("Resistance Stats")]
    [SerializeField] protected CharacterStat Physical;
    [SerializeField] protected CharacterStat Penetraiting;
    [SerializeField] protected CharacterStat Exploding;
    [SerializeField] protected CharacterStat Fire;
    [SerializeField] protected CharacterStat Cold;
    [SerializeField] protected CharacterStat Poison;
    [SerializeField] protected CharacterStat Electricity;
    [SerializeField] protected CharacterStat Radiation;
    [SerializeField] protected CharacterStat Corrosion;
    [SerializeField] protected CharacterStat Spores;
    [SerializeField] protected CharacterStat Toxins;
    [SerializeField] protected CharacterStat Virus;
    [SerializeField] protected CharacterStat Bacterium;
    [SerializeField] protected CharacterStat DarkSubstance;

    [Header("Other")]
    [SerializeField] private UI_Inventory _uiInventory;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField] private float _distanceToGround = 0.1f;
    [SerializeField] private GameObject _characterSprite;

    private Aim _aim;
    private Rigidbody _rigidbody;
    private Inventory _inventory;
    private BoxCollider _characterCollider;
    private Animator _animator;

    public Vector3 MoveDirection { get; set; }
    public bool IsGrounded { get; private set; }
    private readonly Vector3 jumpDirection = Vector3.up;


    private MovementState _movementState;
    public MovementState MovementState
    {
        get
        {
            return _movementState;
        }
        set
        {
            if (_movementState != MovementState.Jump || _movementState != MovementState.JumpBackwards && IsGrounded)
            {
                _movementState = value;
                _animator?.Play(value.ToString());
            }
            else if (value == MovementState.Jump || value == MovementState.JumpBackwards && IsGrounded)
            {
                _movementState = value;
                _animator?.Play(value.ToString());
            }
        }
    }

    private DirectionState _directionState;
    public DirectionState DirectionState
    {
        get { return _directionState; }
        set
        {
            _characterSprite.transform.localScale = Directions.Values[_directionState];
            _directionState = value;
        }
    }

    public bool IsMoveBackwards(Vector3 direction)
    {
        bool isMoveBacwards = false;
        if (DirectionState == DirectionState.Left && direction.x > 0 ||
            DirectionState == DirectionState.Right && direction.x < 0)
        {
            isMoveBacwards = true;
        }
        return isMoveBacwards;
    }

    public void DropWeapon()
    {

    }

    public void Shoot()
    {
        _aim.Shoot();
    }

    public void OnChangeLookAtTarget(Vector3 target)
    {
        Vector3 aimDirection = (target - transform.position).normalized;
        float angle = Mathf.Atan2(aimDirection.z, aimDirection.x) * Mathf.Rad2Deg - 180f;
        DirectionState = transform.position.x <= target.x ? DirectionState.Right : DirectionState.Left;
        _aim.ChangeAimRotation(DirectionState, angle);
    }

    protected void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (_rigidbody == null)
            throw new NullReferenceException("Character doesn't have a Rigidbody!");

        _characterCollider = GetComponent<BoxCollider>();
        if (_characterCollider == null)
            throw new NullReferenceException("Character doesn't have a Collision");

        _aim = GetComponentInChildren<Aim>();
        if (_aim == null)
            throw new NullReferenceException("Character doesn't have a Rigidbody!");

        _animator = GetComponentInChildren<Animator>();
        if (_animator == null)
            throw new NullReferenceException("Character doesn't have an Animator!");

        _inventory = new Inventory();
        //_uiInventory.SetInventory(_inventory);
    }


    private void FixedUpdate()
    {
        float speed = default(float);
        if (_movementState == MovementState.Jump || _movementState == MovementState.JumpBackwards && IsGrounded)
        {
            Jump();
        }
        else if (_movementState == MovementState.Walk)
        {
            speed = walkSpeed.Value;
        }
        else if (_movementState == MovementState.WalkBackwards)
        {
            speed = walkBackwardsSpeed.Value;
        }
        else if (_movementState == MovementState.Run)
        {
            speed = runSpeed.Value;
        }
        else if (_movementState == MovementState.RunBackwards)
        {
            speed = runBackwardsSpeed.Value;
        }
        else if (_movementState == MovementState.Idle)
        {
        }

        Move(speed);
    }

    private void Move(float moveSpeed)
    {
        var offset = new Vector3(MoveDirection.x, 0f, MoveDirection.z) * (MoveDirection.x != 0 && MoveDirection.z != 0 ? moveSpeed / 2 : moveSpeed);
        _rigidbody.MovePosition(_rigidbody.position + offset * Time.deltaTime);
    }

    private void Jump()
    {
        if (IsGrounded)
        {
            _rigidbody.AddForce(jumpDirection + MoveDirection * jumpForce.Value, ForceMode.Impulse);
            //_rigidbody.velocity = (direction + Vector3.up * jumpForce.Value * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var ground = collision.gameObject.GetComponentInParent<Ground>();
        if (ground)
        {
            IsGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        var ground = collision.gameObject.GetComponentInParent<Ground>();
        if (ground)
        {
            IsGrounded = false;
        }
    }

    //private bool IsGrounded()
    //{
    //    Vector3 colliderBottom = new Vector3(_characterCollider.bounds.center.x, _characterCollider.bounds.min.y, _characterCollider.bounds.center.z);
    //    bool grounded = Physics.CheckCapsule(_characterCollider.bounds.center, colliderBottom, _distanceToGround, _groundLayer, QueryTriggerInteraction.Ignore);
    //    return grounded;
    //}
}
