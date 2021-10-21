using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    public float topPosition;
    public float sideBounds;
    [SerializeField] private List<NextBlockProvider> _nextBlockProviders;
    [SerializeField] private DragManager _dragManager;
    [SerializeField] private GameEndController _gameEndController;

    private Block lastBlock;

    private bool enableTouch = true;

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 1, 0.5f);
        Gizmos.DrawCube(new Vector3(0,transform.position.y + topPosition,0), Vector3.one);
        Gizmos.DrawCube(transform.position, new Vector3(sideBounds*2, topPosition*2));
    }
    
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && enableTouch)
        {
           // Vector2 position = new Vector2(GetTouchPosition().x, transform.position.y + topPosition);
           // SpawnShape(_nextBlockProvider.NextBlock, position);

           var touchPoint = GetTouchPosition();
         //  var primitive = GameObject.CreatePrimitive(PrimitiveType.Sphere);
      //     primitive.transform.position = touchPoint;
          foreach (NextBlockProvider nextBlockProvider in _nextBlockProviders)
          {
              if(nextBlockProvider.NextBlockCollider.OverlapPoint(touchPoint))
              {
                  lastBlock = _dragManager.StartDragInteraciton(touchPoint, nextBlockProvider);
                  lastBlock.OnDrop += _gameEndController.OnGameEnded;
                  StartCoroutine(WaitForStability());
                  break;
              }
          }
           
        }
    }

    IEnumerator WaitForStability()
    {
        foreach (NextBlockProvider nextBlockProvider in _nextBlockProviders)
        {
            nextBlockProvider.Clear();
        }
        enableTouch = false;

        yield return new  WaitUntil(()=>Input.GetMouseButtonUp(0));
        
        float timer = 1;
        while (timer > 0)
        {
            if (lastBlock.IsStable())
            {
                timer -= 0.1f;
            }
            else
            {
                timer = 1f;
            }

            yield return new WaitForSeconds(0.1f);
        }

        enableTouch = true;
        foreach (NextBlockProvider nextBlockProvider in _nextBlockProviders)
        {
            nextBlockProvider.Reset();
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
