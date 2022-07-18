using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Bonfire : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;

    private int _startHealth = 60;
    private int _maxHealth;
    private int _health;
    private WaitForSeconds _delay = new WaitForSeconds(1);

    public int Health => _health;
    public int MaxHealth => _maxHealth;

    public event UnityAction OnHealthChanged;
    public event UnityAction Died;

    private void Awake()
    {
        _maxHealth = _startHealth;
        _health = _startHealth;
    }

    private void Start()
    {
        StartCoroutine(LoseHealth());
    }

    private IEnumerator LoseHealth()
    {
        while (true)
        { 
            yield return _delay;

            _health -= 1;

            if (_health < 0)
                StopGame();

            OnHealthChanged?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _health += player.GetPoints();
            _health = Mathf.Clamp(_health, _health, _maxHealth);

            OnHealthChanged?.Invoke();
        }
    }

    private void StopGame()
    {
        _particle.gameObject.SetActive(false);
        Died?.Invoke();
    }

    public void Restart()
    {
        _health = _startHealth;
        _maxHealth = _startHealth;
        _particle.gameObject.SetActive(true);
    }
}