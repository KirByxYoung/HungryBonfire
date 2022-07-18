using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class HealthViewer : MonoBehaviour
{
    [SerializeField] private Bonfire _bonfire;

    private float _speed;
    private float _startSpeed;
    private float _fastSpeed;

    private Slider _slider;

    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        _bonfire.HealthChanged += ChangeHealth;  
    }

    private void OnDisable()
    {
        _bonfire.HealthChanged -= ChangeHealth;
    }

    private void Start()
    {
        _slider.maxValue = _bonfire.MaxHealth;
        _slider.value = _bonfire.Health;

        _speed = 1;
        _fastSpeed = 7;
        _startSpeed = _speed;
    }

    private void ChangeHealth()
    {
        StartCoroutine(ChangeValue());
    }

    private IEnumerator ChangeValue()
    {
        while (_slider.value != _bonfire.Health)
        {
            ChangeSpeed();

            _slider.value = Mathf.MoveTowards(_slider.value, _bonfire.Health, _speed * Time.deltaTime);

            yield return null;
        }
    }

    private void ChangeSpeed()
    {
        if (_slider.value < _bonfire.Health)
            _speed = _fastSpeed;
        else
            _speed = _startSpeed;
    }
}