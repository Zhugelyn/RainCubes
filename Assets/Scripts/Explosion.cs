using UnityEngine;
using System.Collections.Generic;

public class Explosion : MonoBehaviour
{
    private float _minExplosionForce = 0;

    public void Explode(Vector3 explosionPosition, float explosionRadius, float maxExplosionForce)
    {
        foreach (var item in GetExplodableObjects(explosionPosition, explosionRadius))
        {
            var explosionForce = CalculateExplosionForceFromDistance(explosionPosition,
                item.transform.position,
                explosionRadius,
                maxExplosionForce);

            item.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
        }
    }

    private List<Rigidbody> GetExplodableObjects(Vector3 position, float explosionRadius)
    {
        Collider[] hits = Physics.OverlapSphere(position, explosionRadius);

        var explodableObjects = new List<Rigidbody>();

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out Rigidbody _rigidbody))
                explodableObjects.Add(hit.attachedRigidbody);
        }

        return explodableObjects;
    }

    private float CalculateExplosionForceFromDistance(Vector3 firstPostion,
        Vector3 secondPosition,
        float explosionRadius,
        float maxExplosionForce)
    {
        var distance = Mathf.Abs(Vector3.Distance(firstPostion, secondPosition));

        if (distance == 0)
            return maxExplosionForce;

        var delta = 1 - (distance / explosionRadius);

        return Mathf.Lerp(_minExplosionForce, maxExplosionForce, delta);
    }
}
