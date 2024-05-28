using System.Collections;
using UnityEngine;

public class SpawnerCube : Spawner<Cube>
{
    [SerializeField] private float _delay;

    private void Start()
    {
        StartCoroutine(StartCreating());
    }

    private IEnumerator StartCreating()
    {
        var wait = new WaitForSeconds(_delay);

        while (enabled)
        {
            Pool.Get();

            yield return wait;
        }
    }
}
