using System;
using System.Collections;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private float _pollutionModifier;

    private Vector2 _frozenVelocity;
    private float _frozenAngularVelocity;

    private bool _isFrozen;

    public Action<String> OnDrop;

    public float PollutionModifier => _pollutionModifier;

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
        if (!_isFrozen && Mathf.Abs(transform.position.y-Camera.main.transform.position.y) > 15)
        {
            OnDrop.Invoke("You dropped it");
        }

        if (!_isFrozen)
        {
            var isStable = IsStable();
            _spriteRenderer.material.SetColor("_OutlineColor", isStable ? Color.green : Color.red);
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

    public void PlayFreezeAnimation()
    {
        _spriteRenderer.material.SetColor("_OutlineColor", Color.blue);
        // StartCoroutine(FreezeAnimation());
    }
    
    private IEnumerator FreezeAnimation()
    {
        var freezeTime = 0.4f;
        var timeElapsed = 0f;
        var mat = _spriteRenderer.material;
        while (timeElapsed < freezeTime)
        {
            timeElapsed += Time.deltaTime;
            mat.SetFloat("_freezeAmount", timeElapsed / freezeTime);
            yield return null;
        }
        mat.SetFloat("_freezeAmount", 1f);
    }
}