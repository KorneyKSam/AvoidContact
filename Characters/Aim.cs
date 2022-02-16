using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] private Transform _aimTransform;
    [SerializeField] private Transform _headBone;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private Collider _shooterCollider;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _bulletForce;

    public void ChangeAimRotation(DirectionState directionState, float angle)
    {
        switch (directionState)
        {
            case DirectionState.Left:
                if (_aimTransform != null)
                    _aimTransform.localEulerAngles = new Vector3(0, 0, angle);
                if (_headBone != null)
                    _headBone.localEulerAngles = new Vector3(0, 0, angle);
                break;
            case DirectionState.Right:
                if (_aimTransform != null)
                    _aimTransform.localEulerAngles = new Vector3(-180, -180, -angle);
                if (_headBone != null)
                    _headBone.localEulerAngles = new Vector3(-180, -180, -angle);
                break;
            default:
                break;
        }
    }

    public void Shoot()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _firePoint.position, _aimTransform.transform.rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        Collider bulletCollider = bullet.GetComponent<Collider>();
        Physics.IgnoreCollision(bulletCollider, _shooterCollider);
        //bulletRigidbody.AddForce(_firePoint.forward * _bulletForce, ForceMode.Impulse);
        bulletRigidbody.velocity = _firePoint.forward * _bulletForce;
    }

    public void OnChangedPosition(Vector3 target)
    {
        _firePoint.LookAt(target);
    }

}
