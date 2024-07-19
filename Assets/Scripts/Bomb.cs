using System;
using System.Collections;
using UnityEngine;

public class Bomb : MonoBehaviour, IInitialized, IDeactivable<Bomb>
{
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
                Deactivation?.Invoke(this);

            counter++;

            yield return wait;
        }
    }
}
