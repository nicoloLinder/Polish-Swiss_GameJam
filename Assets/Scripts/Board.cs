using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Board : MonoBehaviour
{
    private bool _isFrozen;

    private List<Block> _blocks;
    private Camera _camera;

    public int BlocksPlaced { get; private set; } = 0;

    public event Action<Block> NotifyBlockAdded; 
    
    public bool IsFrozen
    {
        get => _isFrozen;
        set
        {
            _isFrozen = value;
            UpdateFrozenStatus();
            if (!_isFrozen)
            {
                CheckTallestBlock();
            }
        }
    }

    private void Start()
    {
        _camera = Camera.main;
    }

    private void UpdateFrozenStatus()
    {
        foreach (Block block in _blocks)
        {
            if (block.IsStable())
            {
                block.FreezeBlock(_isFrozen);
            }
        }
    }

    private void FreezeBlock(GameObject block, bool isFrozen)
    {
    }

    private void Awake()
    {
        _blocks = new List<Block>();
    }

    public void AddBlock(Block block)
    {
        BlocksPlaced++;
        _blocks.Add(block);
        NotifyBlockAdded?.Invoke(block);
    }


    public void RemoveBlock(Block block)
    {
        _blocks.Remove(block);
    }

    public void CheckTallestBlock()
    {
        List<Block> heigtList = _blocks.OrderBy(block => block.transform.position.y).ToList();

        if (heigtList.Count > 0)
        {
            if (heigtList[heigtList.Count - 1].transform.position.y > _camera.transform.position.y)
            {
                _camera.transform.position += Vector3.up;
            }

            List<Block> blocksMarkedForRemoval = new List<Block>();
            
            foreach (Block sortedObject in heigtList)
            {
                if (sortedObject.IsStable())
                {
                    // sortedObject.SetColor(Color.green);/**/
                    if (sortedObject.transform.position.y < _camera.transform.position.y - 4)
                    {
                        blocksMarkedForRemoval.Add(sortedObject);
                    }
                }
                else
                {
                    // sortedObject.SetColor(Color.red);
                }

            }

            foreach (Block markedBlock in blocksMarkedForRemoval)
            {
                markedBlock.SetColor(Color.black);
                markedBlock.FreezeBlock(true);
                RemoveBlock(markedBlock);
            }
        }
    }
}