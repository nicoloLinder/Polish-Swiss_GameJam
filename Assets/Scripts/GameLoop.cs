using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public float topPosition;
    public float sideBounds;
    [SerializeField] private NextBlockProvider _nextBlockProvider;
    [SerializeField] private DragManager _dragManager;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1, 0.5f);
        Gizmos.DrawCube(new Vector3(0,transform.position.y + topPosition,0), Vector3.one);
        Gizmos.DrawCube(transform.position, new Vector3(sideBounds*2, topPosition*2));
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
           // Vector2 position = new Vector2(GetTouchPosition().x, transform.position.y + topPosition);
           // SpawnShape(_nextBlockProvider.NextBlock, position);

           var touchPoint = GetTouchPosition();
         //  var primitive = GameObject.CreatePrimitive(PrimitiveType.Sphere);
      //     primitive.transform.position = touchPoint;
            if(_nextBlockProvider.NextBlockCollider.OverlapPoint(touchPoint))
            {
                _dragManager.StartDragInteraciton(touchPoint);
            }
        }
    }

    void SpawnShape(GameObject shape, Vector3 position)
    {
        Instantiate(shape, position, Quaternion.identity);
    }

    Vector2 GetTouchPosition()
    {
        return  Camera.main.ScreenToWorldPoint(Input.mousePosition);
        
        var actualPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        var clampedValue = Mathf.Clamp(actualPosition.x, -sideBounds, sideBounds);
        return new Vector2((int) clampedValue, actualPosition.y);
    }
}
