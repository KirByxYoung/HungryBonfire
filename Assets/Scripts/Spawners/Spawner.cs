using System.Collections;
using UnityEngine;

public abstract class Spawner : ObjectsPool
{
    [SerializeField] private float _spawnRange;
    [SerializeField] private float _delayTime;
    [SerializeField] private Bonfire _bonfire;

    protected Quaternion Rotation;

    private WaitForSeconds _delay;
    private Vector3 _startPoint;

    private void Awake()
    {
        _startPoint = transform.position;
        _delay = new WaitForSeconds(_delayTime);

        Initialize();
    }

    private void OnEnable()
    {
        _bonfire.Died += Restart;
    }

    private void OnDisable()
    {
        _bonfire.Died -= Restart;
    }

    protected abstract void SpawnAll();

    protected void Spawn()
    {
        if (TryGetObject(out GameObject gameObject))
        {
            gameObject.SetActive(true);
            gameObject.transform.rotation = Rotation;
            gameObject.transform.position = GetRandomSpawnPoint();
        }
    }

    protected Vector3 GetRandomSpawnPoint()
    {
        return new Vector3(Random.Range(_startPoint.x - _spawnRange, _startPoint.x + _spawnRange), _startPoint.y, Random.Range(_startPoint.z - _spawnRange, _startPoint.z + _spawnRange));
    }

    protected IEnumerator SpawnAfterTime()
    {
        while (true)
        {
            Spawn();

            yield return _delay;
        }
    }

    protected float GetRandomRotationY()
    {
        return Random.Range(0, 360);
    }

    private void Restart()
    {
        ResetPool();
        SpawnAll();
    }
}