using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameLoop : MonoBehaviour
{

    public List<GameObject> shapes;
    public float topPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(new Vector3(0,topPosition,0), Vector3.one);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.GetMouseButton(0));
        if (Input.touchCount > 0)
        {
            Vector2 position = new Vector2(GetTouchPosition().x, topPosition);
            SpawnShape(shapes[0], position);
        }
    }

    void SpawnShape(GameObject shape, Vector3 position)
    {
        Instantiate(shape, position, Quaternion.identity);
    }

    Vector2 GetTouchPosition()
    {
        Vector2 position = Input.mousePosition;
        return touchPosition;
    }
    
}
