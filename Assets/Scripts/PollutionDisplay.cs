using UnityEngine;

public class PollutionDisplay : MonoBehaviour
{
    [SerializeField] private float _arrowSpeed;
    [SerializeField] private float _startingPollution;
    [SerializeField] private float _maxPollution;
    [SerializeField] private Board _board;
    [SerializeField] private Transform _arrowTransform;
    [SerializeField] private Transform[] _bounds;
    private float _currentPollution;
    private Quaternion[] _rotations;
    
    private void Awake()
    {
        _currentPollution = _startingPollution;
        _board.NotifyBlockAdded += OnBlockAdded;
        _rotations = new Quaternion[2];
        _rotations[0] =
            Quaternion.LookRotation(Vector3.forward, (_bounds[0].position - _arrowTransform.position).normalized);
        _rotations[1] =
            Quaternion.LookRotation(Vector3.forward, (_bounds[1].position - _arrowTransform.position).normalized);
    }

    private void OnBlockAdded(Block block)
    {
        _currentPollution += block.PollutionModifier;
    }

    private void Update()
    {
        var desiredRotation = Quaternion.Lerp(_rotations[0], _rotations[1], _currentPollution / _maxPollution);
        _arrowTransform.rotation = Quaternion.Lerp(_arrowTransform.rotation, desiredRotation, _arrowSpeed);
    }
}
