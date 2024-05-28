using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour, IInitialized
{
    [SerializeField] private T _prefab;
    [SerializeField] private SpawnZone _spawnZone;
    [SerializeField] private float _heightSpawn;
    [SerializeField] private int _poolMaxSize;

    protected ObjectPool<T> Pool;

    private void Awake()
    {
        Pool = new ObjectPool<T>(
        createFunc: () => Instantiate(_prefab),
        actionOnGet: (obj) => ActionOnGet(obj),
        actionOnRelease: (obj) => obj.gameObject.SetActive(false),
        maxSize: _poolMaxSize);
    }

    public virtual void ActionOnGet(T obj)
    {
        obj.Init(this);
        obj.transform.position = GetSpawnPosition();
        obj.gameObject.SetActive(true);
    }

    public virtual void ReturnPool(T obj)
    {
        Pool.Release(obj);
    }

    private Vector3 GetSpawnPosition()
    {
        var collider = _spawnZone.gameObject.GetComponent<BoxCollider>();
        var maxPosX = _spawnZone.transform.position.x + collider.size.x / 2;
        var minPosX = _spawnZone.transform.position.x - collider.size.x / 2;
        var maxPosZ = _spawnZone.transform.position.z + collider.size.z / 2;
        var minPosZ = _spawnZone.transform.position.z - collider.size.z / 2;

        var posX = Random.Range(minPosX, maxPosX);
        var posY = _spawnZone.transform.position.y + _heightSpawn;
        var posZ = Random.Range(minPosZ, maxPosZ);

        return new Vector3(posX, posY, posZ);
    }
}
