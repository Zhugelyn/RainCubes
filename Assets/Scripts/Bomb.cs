using System;
using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour, IInitialized, IDeactivable<Bomb>, IExplodable
{
    [SerializeField] private Explosion _explosion;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    [SerializeField] private ParticleSystem _particleSystem;
    [SerializeField] private AudioClip _clip;

    private int _minLifetime = 2;
    private int _maxLifetime = 5;

    public event Action<Bomb> Deactivation;

    private Coroutine _deactivate;

    private void OnEnable()
    {
        _deactivate = StartCoroutine(Deactivate());
    }

    private void OnDisable()
    {
        if (_deactivate != null)
            StopCoroutine(Deactivate());
    }

    public void Init(Vector3 position)
    {
        transform.position = position;
    }

    private IEnumerator Deactivate()
    {
        int counter = 0;
        float delay = 1f;
        var wait = new WaitForSeconds(delay);
        var lifetime = UnityEngine.Random.Range(_minLifetime, _maxLifetime);


        while (enabled)
        {
            if (counter >= lifetime)
            {
                Explode();
                Deactivation?.Invoke(this);
            }

            counter++;

            yield return wait;
        }
    }

    public void Explode()
    {
        var explosionSystem = Instantiate(_particleSystem, transform.position, Quaternion.identity);
        explosionSystem.Play();

        _explosion.Explode(transform.position, _explosionRadius, _explosionForce);
        AudioSource.PlayClipAtPoint(_clip, transform.position);
        Destroy(explosionSystem);
    }
}
