using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class ObjectsPool : MonoBehaviour
{
    [SerializeField] private GameObject _container;
    [SerializeField] private List<GameObject> _templates;
    [SerializeField] private int _capacity;

    private List<GameObject> _pool = new List<GameObject>();

    protected int Capacity => _capacity;

    protected void Initialize()
    {
        for (int i = 0; i < _capacity; i++)
        {
            GameObject newPrefab = GetRandomPrefab();

            GameObject spawned = Instantiate(newPrefab, _container.transform);
            spawned.SetActive(false);

            _pool.Add(spawned);
        }
    }

    protected bool TryGetObject(out GameObject result)
    {
        result = _pool.FirstOrDefault(p => p.activeSelf == false);

        return result != null;
    }

    private GameObject GetRandomPrefab()
    {
        int index = Random.Range(0, _templates.Count);

        return _templates[index];
    }

    protected void ResetPool()
    {
        foreach (var item in _pool)
        {
            item.SetActive(false);
            item.transform.position = _container.transform.position;
        }
    }
}