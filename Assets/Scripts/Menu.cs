using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField] private GameObject _playButtonPrefab;
    [SerializeField] private GameObject _creditsButtonPrefab;
    [SerializeField] private GameObject _logoPrefab;

    [SerializeField] private List<GameObject> _spawnedObjects;
    [SerializeField] private List<GameObject> _objectsToEnable;
    [SerializeField] private List<GameObject> _objectsToDisable;
    [SerializeField] private List<GameObject> _objectsToSpawn;
    [SerializeField] private Transform _spawnPoint;

    private Collider2D _creditsCollider;
    private Collider2D _playCollider;
    private Camera _camera;
    void Start()
    {
        _camera = Camera.main;
        StartCoroutine(MenuAnimation());
        for (int i = 0; i < _objectsToEnable.Count; i++)
        {
            _objectsToEnable[i].SetActive(false);
        }
    }


    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = _camera.ScreenPointToRay(Input.mousePosition);
            if (_creditsCollider != null && _creditsCollider.OverlapPoint(ray.origin))
            {

            }
            else if (_playCollider != null && _playCollider.OverlapPoint(ray.origin))
            {
                for (int i = 0; i < _spawnedObjects.Count; i++)
                {
                    _spawnedObjects[i].GetComponent<Rigidbody2D>()
                        .AddForce(new Vector2(Random.Range(-1f, 1f), 1f) * 10000f);
                    Destroy(_spawnedObjects[i], 3f);
                }

                for (int i = 0; i < _objectsToEnable.Count; i++)
                {
                    _objectsToEnable[i].SetActive(true);
                }
                
                for (int i = 0; i < _objectsToDisable.Count; i++)
                {
                    _objectsToDisable[i].SetActive(false);
                }
            }
        }
    }

    private IEnumerator MenuAnimation()
    {
        _spawnedObjects.Add(Instantiate(_logoPrefab, _spawnPoint.position, Quaternion.Euler(Vector3.forward * Random.value * 360f)));
        yield return new WaitForSeconds(Random.Range(.9f, 1.5f));
        for (int i = 0; i < _objectsToSpawn.Count; i++)
        {
            _spawnedObjects.Add(Instantiate(_objectsToSpawn[i], _spawnPoint.position, Quaternion.Euler(Vector3.forward * Random.value * 360f)));
            yield return new WaitForSeconds(Random.Range(.6f, 1.5f));
        }
        _spawnedObjects.Add(Instantiate(_creditsButtonPrefab, _spawnPoint.position, Quaternion.Euler(Vector3.forward * Random.value * 360f)));
        _creditsCollider = _spawnedObjects[_spawnedObjects.Count - 1].GetComponent<Collider2D>();
        yield return new WaitForSeconds(Random.Range(.6f, 1.5f));
        _spawnedObjects.Add(Instantiate(_playButtonPrefab, _spawnPoint.position, Quaternion.Euler(Vector3.forward * Random.value * 360f)));
        _playCollider = _spawnedObjects[_spawnedObjects.Count - 1].GetComponent<Collider2D>();
    }
}
