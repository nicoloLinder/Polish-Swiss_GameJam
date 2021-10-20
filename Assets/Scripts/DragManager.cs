using UnityEngine;

public class DragManager : MonoBehaviour
{
    [SerializeField] private NextBlockProvider _blockProvider;
    [SerializeField] private Board _board;
    private bool _dragging;
    private TargetJoint2D _joint;
    private Camera _mainCam;
    private GameObject _currentBlock;

    private void Awake()
    {
        _mainCam = Camera.main;
    }

    public void StartDragInteraciton(Vector2 touchPoint)
    {
        _board.IsFrozen = true;
        _dragging = true;
        var position = _blockProvider.NextBlockCollider.transform.position;
         _currentBlock = Instantiate(_blockProvider.NextBlock);
         _currentBlock.transform.position = position;
        _joint = _currentBlock.AddComponent<TargetJoint2D>();
        _joint.anchor = _currentBlock.transform.InverseTransformPoint(touchPoint);
        _joint.dampingRatio = 10f;
        _joint.frequency = 15f;
        _joint.maxForce = 4000;
    }

    private void Update()
    {
        if (_dragging)
        {
            var ray = _mainCam.ScreenPointToRay(Input.mousePosition);
            _joint.target = ray.origin;
            if (Input.GetMouseButtonUp(0))
            {
                _dragging = false;
                Destroy(_joint);
                _board.IsFrozen = false;
                _board.AddBlock(_currentBlock);
                var decay = _currentBlock.AddComponent<Decay>();
                decay._board = _board;
                _currentBlock = null;
            }
        }
    }
}