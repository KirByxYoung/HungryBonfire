using System.Collections;
using UnityEngine;

public class Branch : Fuel
{
    private WaitForSeconds _delay = new WaitForSeconds(3);

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Player player))
        {
            player.TakePoints(Points);
            
            Collider.enabled = false;

            StartCoroutine(SetFire());
        }
    }

    private IEnumerator SetFire()
    {
        GameObject fire = Instantiate(Particle, transform.position, Quaternion.identity, transform).gameObject;

        yield return _delay;

        Destroy(fire);

        Die();
    }

    protected override void Die()
    {
        Collider.enabled = true;
        gameObject.SetActive(false);
    }
}