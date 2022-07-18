using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [SerializeField] private int _points;
    [SerializeField] private int _maxPoints;

    private Vector3 _startPosition;

    public int Points => _points;
    public int MaxPoints => _maxPoints;

    public event UnityAction ChangedPoints;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    public void TakePoints(int points)
    {
        _points += points;

        _points = Mathf.Clamp(_points, 0, _maxPoints);

        ChangedPoints?.Invoke();
    }

    public int GetPoints()
    {
        int points = _points;

        _points = 0;

         ChangedPoints?.Invoke();

        return points;
    }

    public void Restart()
    {
        _points = 0;
        ChangedPoints?.Invoke();
        transform.position = _startPosition;
    }
}