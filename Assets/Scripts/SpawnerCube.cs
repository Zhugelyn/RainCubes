using System.Collections;
using UnityEngine;

public class SpawnerCube : Spawner<Cube>
{
    [SerializeField] private float _delay;
    [SerializeField] private Cube _cube;

    private void OnEnable()
    {
        _cube.Deactivation += ReturnCube;
    }

    private void OnDisable()
    {
        _cube.Deactivation -= ReturnCube;
    }

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

    private void ReturnCube(Cube cube)
    {
        ReturnToPool(cube);
        Debug.Log("Release");
    }
}
