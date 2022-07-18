using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class PlayerPointsViewer : MonoBehaviour
{
    [SerializeField] private Player _player;

    private Slider _slider;
    private float _speed;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _player.ChangedPoints += ChangePoints;
    }

    private void OnDisable()
    {
        _player.ChangedPoints -= ChangePoints;
    }

    private void Start()
    {
        _speed = 15;
        _slider.maxValue = _player.MaxPoints;
    }

    private void ChangePoints()
    {
        StartCoroutine(ChangeValue());
    }

    private IEnumerator ChangeValue()
    {
        while (_slider.value != _player.Points)
        {
            _slider.value = Mathf.MoveTowards(_slider.value, _player.Points, _speed * Time.deltaTime);

            yield return null;
        }
    }
}