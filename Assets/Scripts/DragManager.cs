using UnityEngine;

public class DragManager : MonoBehaviour
{
    [SerializeField] private NextBlockProvider _blockProvider;
    private bool _dragging;
    private TargetJoint2D _joint;
    private Camera _mainCam;

    private void Awake()
    {
        _mainCam = Camera.main;
    }

    public void StartDragInteraciton(Vector2 touchPoint)
    {
        _dragging = true;
        var position = _blockProvider.NextBlockCollider.transform.position;
        var block = Instantiate(_blockProvider.NextBlock);
        block.transform.position = position;
        _joint = block.AddComponent<TargetJoint2D>();
        _joint.anchor = block.transform.InverseTransformPoint(touchPoint);
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
            }
        }
    }
}