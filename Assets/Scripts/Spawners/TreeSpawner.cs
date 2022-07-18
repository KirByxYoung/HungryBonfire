using UnityEngine;

public class TreeSpawner : Spawner
{
    private void Start()
    {
        SpawnAll();
    }

    protected override void SpawnAll()
    {
        for (int i = 0; i < Capacity; i++)
        {
            Rotation = Quaternion.Euler(0, GetRandomRotationY(), 0);
            Spawn();
        }

        StartCoroutine(SpawnAfterTime());
    }
}