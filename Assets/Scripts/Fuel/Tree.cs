using System.Collections;
using UnityEngine;

public class Tree : Fuel
{
    [SerializeField] private Player _player;

    private float _timeForBurning = 5;
    private float _burningTime;
    private float _bigFireTime;
    private float _timeFactorBigFire = 2;
    private bool _isBurning = false;
    private Vector3 _bigFire = new Vector3(0.25f, 0.7f, 0.25f);
    private Vector3 _smallFire = new Vector3(0.2f, 0.3f, 0.2f);
    private float _fireRange = 0.2f;
    private Vector3 _firePosition;

    private void Start()
    {
        _burningTime = _timeForBurning;
        _bigFireTime = _timeForBurning / _timeFactorBigFire;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _player = player;
            _isBurning = true;
            StartCoroutine(SetFire());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            _isBurning = false;
        }
    }

    private IEnumerator SetFire()
    {
        SetFirePosition();

        GameObject fire = Instantiate(Particle, _firePosition, Quaternion.identity, transform).gameObject;

        while (_isBurning)
        {
            ChangeFireScale(fire, _smallFire);
            _burningTime -= Time.deltaTime;

            if (_burningTime < _bigFireTime)
                ChangeFireScale(fire, _bigFire);

            if (_burningTime < 0)
            {
                _player.TakePoints(Points);
                SetStartBurningTime();
                Destroy(fire);
                Die();
            }

            yield return null;
        }

        ChangeFireScale(fire, _smallFire);
        Destroy(fire);
        SetStartBurningTime();

        yield return null;
    }

    protected override void Die()
    {
        gameObject.SetActive(false);
    }

    private void ChangeFireScale(GameObject fire, Vector3 scale)
    {
        fire.gameObject.transform.localScale = scale;
    }

    private void SetStartBurningTime()
    {
        _burningTime = _timeForBurning;
    }

    private void SetFirePosition()
    {
        _firePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z - _fireRange);
    }
}