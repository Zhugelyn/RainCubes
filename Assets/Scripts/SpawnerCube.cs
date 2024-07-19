using System;
using System.Collections;
using UnityEngine;

public class SpawnerCube : Spawner<Cube>
{

    [SerializeField] private SpawnZone _spawnZone;
    [SerializeField] private float _heightSpawn;

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
            var cube = Pool.Get();
            cube.Init(GetSpawnPosition());
            cube.gameObject.SetActive(true);

            yield return wait;
        }
    }

    private Vector3 GetSpawnPosition()
    {
        var collider = _spawnZone.gameObject.GetComponent<BoxCollider>();
        var maxPosX = _spawnZone.transform.position.x + collider.size.x / 2;
        var minPosX = _spawnZone.transform.position.x - collider.size.x / 2;
        var maxPosZ = _spawnZone.transform.position.z + collider.size.z / 2;
        var minPosZ = _spawnZone.transform.position.z - collider.size.z / 2;

        var posX = UnityEngine.Random.Range(minPosX, maxPosX);
        var posY = _spawnZone.transform.position.y + _heightSpawn;
        var posZ = UnityEngine.Random.Range(minPosZ, maxPosZ);

        return new Vector3(posX, posY, posZ);
    }
}
