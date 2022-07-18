using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class Fuel : MonoBehaviour
{
    [SerializeField] protected ParticleSystem Particle;
    [SerializeField] protected int Points;

    protected Collider Collider;

    private void Awake()
    {
        Collider = GetComponent<Collider>();
    }

    protected abstract void Die();
}