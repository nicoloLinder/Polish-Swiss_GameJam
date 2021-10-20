using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{

    public List<GameObject> shapes;
    public float topPosition;

    [SerializeField] private NextBlockProvider _nextBlockProvider;


    private void OnDrawGizmos()
    {
        Gizmos.DrawCube(new Vector3(0,topPosition,0), Vector3.one);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 position = new Vector2(GetTouchPosition().x, topPosition);
            SpawnShape(_nextBlockProvider.NextBlock, position);
        }
    }

    void SpawnShape(GameObject shape, Vector3 position)
    {
        Instantiate(shape, position, Quaternion.identity);
    }

    Vector2 GetTouchPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    
}
