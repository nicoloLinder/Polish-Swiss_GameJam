using UnityEngine;

public class ObjectRotController : MonoBehaviour
{
    [SerializeField] private int _rotStatus;
    [SerializeField] private int _maxRotAmount;

    public bool AdvanceRotting()
    {
        _rotStatus++;
        return CheckRotStatus();
    }

    private bool CheckRotStatus()
    {
        if (_rotStatus >= _maxRotAmount)
        {
            RotObject();
            return true;
        }

        return false;
    }

    private void RotObject()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.black;
    }
}