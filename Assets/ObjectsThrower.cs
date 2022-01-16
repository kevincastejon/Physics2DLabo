using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsThrower : MonoBehaviour
{
    [SerializeField] private Transform _transformA;
    [SerializeField] private Transform _transformB;
    [SerializeField] private float _speed = 3f;

    private bool _isTriggerA;
    private bool _hasRigidbody2DA;
    private bool _isKinematicA;
    private bool _isTriggerB;
    private bool _hasRigidbody2DB;
    private bool _isKinematicB;

    private Rigidbody2D _rigidbodyA;
    private Rigidbody2D _rigidbodyB;
    private Collider2D _colliderA;
    private Collider2D _colliderB;

    private bool _isStarted;

    private void Awake()
    {
        _colliderA = _transformA.GetComponent<Collider2D>();
        _colliderB = _transformB.GetComponent<Collider2D>();
    }

    public void ThrowObjects(bool isTriggerA, bool hasRigidbody2DA, bool isKinematicA, bool isTriggerB, bool hasRigidbody2DB, bool isKinematicB)
    {
        _isStarted = true;

        _isTriggerA = isTriggerA;
        _hasRigidbody2DA = hasRigidbody2DA;
        _isKinematicA = isKinematicA;
        _isTriggerB = isTriggerB;
        _hasRigidbody2DB = hasRigidbody2DB;
        _isKinematicB = isKinematicB;

        _colliderA.isTrigger = _isTriggerA;
        _colliderB.isTrigger = _isTriggerB;

        if (_hasRigidbody2DA)
        {
            _rigidbodyA = _transformA.gameObject.AddComponent<Rigidbody2D>();
            _rigidbodyA.isKinematic = _isKinematicA;
            _rigidbodyA.gravityScale = 0f;
            _rigidbodyA.angularDrag = 0f;
            _rigidbodyA.interpolation = RigidbodyInterpolation2D.Interpolate;
        }
        if (_hasRigidbody2DB)
        {
            _rigidbodyB = _transformB.gameObject.AddComponent<Rigidbody2D>();
            _rigidbodyB.isKinematic = _isKinematicB;
            _rigidbodyB.gravityScale = 0f;
            _rigidbodyB.angularDrag = 0f;
            _rigidbodyB.interpolation = RigidbodyInterpolation2D.Interpolate;
        }
        PhysicsThrow();
    }

    private void PhysicsThrow()
    {
        if (_hasRigidbody2DA && !_isKinematicA)
        {
            _rigidbodyA.AddForce(Vector3.right * _speed, ForceMode2D.Impulse);
        }
        if (_hasRigidbody2DB && !_isKinematicB)
        {
            _rigidbodyB.AddForce(Vector3.left * _speed, ForceMode2D.Impulse);
        }
    }

    private void Update()
    {
        if (!_isStarted)
        {
            return;
        }

        if (!_hasRigidbody2DA)
        {
            _transformA.Translate(Vector3.right * _speed * Time.deltaTime);
        }
        if (!_hasRigidbody2DB)
        {
            _transformB.Translate(Vector3.left * _speed * Time.deltaTime);
        }
    }

    private void FixedUpdate()
    {
        if (!_isStarted)
        {
            return;
        }

        if (_hasRigidbody2DA && _isKinematicA)
        {
            _rigidbodyA.MovePosition(_rigidbodyA.position + Vector2.right * _speed * Time.fixedDeltaTime);
        }
        if (_hasRigidbody2DB && _isKinematicB)
        {
            _rigidbodyB.MovePosition(_rigidbodyB.position + Vector2.left * _speed * Time.fixedDeltaTime);
        }
    }

    public void ResetObjects()
    {
        _isStarted = false;
        DestroyImmediate(_rigidbodyA);
        DestroyImmediate(_rigidbodyB);
        _transformA.position = new Vector3(-4.5f, 0f, 0f);
        _transformA.rotation = Quaternion.identity;
        _transformB.position = new Vector3(4.5f, 0f, 0f);
        _transformB.rotation = Quaternion.identity;
    }
}
