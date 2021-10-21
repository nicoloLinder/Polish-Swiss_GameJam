using UnityEngine;
using Random = UnityEngine.Random;

public class NextBlockProvider : MonoBehaviour
{
    [SerializeField] private Transform _lookupParent;
    [SerializeField] private GameObject[] _prefabs;
    
    private GameObject _currentPrefab;

    public Collider2D NextBlockCollider { get; set; }
    public GameObject NextBlock
    {
        get
        {
            var retVal = _currentPrefab;
            NextBlock = _prefabs[Random.Range(0, _prefabs.Length)];
            return retVal;
        }
        private set
        {
            _currentPrefab = value;
            DisplayNextBlock();
        }
    }

    private void Awake()
    {
        NextBlock = _prefabs[Random.Range(0, _prefabs.Length)];
    }

    private void DisplayNextBlock()
    {
        Clear();

        var lookup = Instantiate(_currentPrefab);
        var rend = lookup.GetComponent<SpriteRenderer>();
        rend.color = new Color(1f,1f,1f,0.5f);
        rend.sortingOrder = -10;
        NextBlockCollider = lookup.GetComponent<Collider2D>();
        NextBlockCollider.isTrigger = true;
        lookup.GetComponent<Rigidbody2D>().isKinematic = true;
        lookup.transform.parent = _lookupParent;
        lookup.transform.localPosition = Vector3.zero;
    }

    public void Reset()
    {
        NextBlock = _prefabs[Random.Range(0, _prefabs.Length)];
    }

    public void Clear()
    {
        if (_lookupParent.childCount > 0)
        {
            for (int i = 0; i < _lookupParent.childCount; i++)
            {
                Destroy(_lookupParent.GetChild(i).gameObject);
            }
        }
    }
}
