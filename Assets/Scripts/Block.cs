using System;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private Vector2 _frozenVelocity;
    private float _frozenAngularVelocity;

    private bool _isFrozen;
    
    public void FreezeBlock(bool isFrozen)
    {
        _rigidbody2D.isKinematic = isFrozen;
        if (isFrozen)
        {
            _isFrozen = true;
            _frozenVelocity = _rigidbody2D.velocity;
            _frozenAngularVelocity = _rigidbody2D.angularVelocity;
            
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.angularVelocity = 0f;
        }
        else
        {
            _isFrozen = false;
            _rigidbody2D.velocity = _frozenVelocity;
            _rigidbody2D.angularVelocity = _frozenAngularVelocity;
        }
       
    }

    private void Update()
    {
        if (IsStable())
        {
            SetColor(Color.green);
            if (_isFrozen)
            {
                SetColor(Color.blue);
            }
        }
        else
        {
            SetColor(Color.red);
        }
    }

    public void SetColor(Color color)
    {
        _spriteRenderer.color = color;
    }

    public bool IsStable()
    {
        return _rigidbody2D.velocity.SqrMagnitude() < 0.1f && _rigidbody2D.angularVelocity < 0.1f;
    }
}