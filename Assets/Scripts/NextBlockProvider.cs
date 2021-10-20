using UnityEngine;
using Random = UnityEngine.Random;

public class NextBlockProvider : MonoBehaviour
{
    [SerializeField] private Transform _lookupParent;
    [SerializeField] private GameObject[] _prefabs;
    private GameObject _currentPrefab;
    
    public GameObject NextBlock
    {
        get
        {
            var retVal = _currentPrefab;
            _currentPrefab = _prefabs[Random.Range(0, _prefabs.Length)];
            return retVal;
        }
        private set => _currentPrefab = value;
    }

    private void Awake()
    {
        NextBlock = _prefabs[Random.Range(0, _prefabs.Length)];
    }

    private void DisplayNextBlock()
    {
        if (_lookupParent.childCount > 0)
        {
            Destroy(_lookupParent.GetChild(0).gameObject);
        }

        var lookup = Instantiate(_currentPrefab);
        lookup.GetComponent<SpriteRenderer>().color = new Color(1f,1f,1f,0.5f);
        lookup.GetComponent<Collider2D>().enabled = false;
        lookup.GetComponent<Rigidbody2D>().isKinematic = true;
    }
}
