using UnityEngine;

public class BranchSpawner : Spawner
{
    private readonly float _angle = -80;

    private void Start()
    {
        SpawnAll();
    }

    protected override void SpawnAll()
    {
        for (int i = 0; i < Capacity; i++)
        {
            Rotation = Quaternion.Euler(_angle, GetRandomRotationY(), 0);
            Spawn();
        }

        StartCoroutine(SpawnAfterTime());
    }
}