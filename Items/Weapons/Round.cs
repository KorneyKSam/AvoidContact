using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round : MonoBehaviour
{
    [SerializeField] private GameObject _hitEffect;
    public Collision _shooter;
    //private float _velocity;
    //private float _energy;
    //private float _roundWeight;
    //private float _powerFactor;

    private void OnCollisionEnter(Collision collision)
    {
        GameObject effect = Instantiate(_hitEffect, transform.position, Quaternion.Euler(40f, 0f, 0f));
        Destroy(effect, 0.1f);
        Destroy(gameObject);
    }
}
