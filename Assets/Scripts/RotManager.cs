using System.Collections.Generic;
using UnityEngine;

public class RotManager
{
    [SerializeField] private List<ObjectRotController> _rotControllers;

    public void AdvanceRot()
    {
        foreach (ObjectRotController rotController in _rotControllers)
        {
            if (rotController.AdvanceRotting())
            {
                Debug.Log("rotten");
            }
        }
    }

    public void AddRotController(ObjectRotController rotController)
    {
        _rotControllers.Add(rotController);
    }

    // public void RemoveRotController();
}