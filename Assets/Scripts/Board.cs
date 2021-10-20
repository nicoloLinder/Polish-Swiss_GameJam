using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    private bool _isFrozen;
    
    private List<GameObject> _blocks;
    public bool IsFrozen
    {
        get => _isFrozen;
        set
        {
            _isFrozen = value;
            UpdateFrozenStatus();
        } 
    }

    private void UpdateFrozenStatus()
    {
        foreach (GameObject block in _blocks)
        {
            var rigidbody = block.GetComponent<Rigidbody2D>();
            rigidbody.isKinematic = _isFrozen;
            rigidbody.velocity = Vector2.zero;
            rigidbody.angularVelocity = 0f;
        }
    }

    private void Awake()
    {
        _blocks = new List<GameObject>();
    }

    public void AddBlock(GameObject block)
    {
        _blocks.Add(block);
    }


    public void RemoveBlock(GameObject block)
    {
        _blocks.Remove(block);
    }
}
