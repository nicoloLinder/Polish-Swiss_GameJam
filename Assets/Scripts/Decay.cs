using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decay : MonoBehaviour
{
    private float _decayTime = 15f;
    public Board _board;
    // void Update()
    // {
    //     // _decayTime -= Time.deltaTime;
    //     if (_decayTime < 0f)
    //     {
    //         Destroy(gameObject);
    //         _board.RemoveBlock(gameObject);
    //     }
    // }
}
