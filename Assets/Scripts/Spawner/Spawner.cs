using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour where T : MonoBehaviour, IInitialized, IDeactivable<T>
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _poolMaxSize;

    protected ObjectPool<T> Pool;

    public event Action<T> Pooling;
    public event Action<T, ObjectPool<T>> ObjectPoolChanged;

    private void Awake()
    {
        Pool = new ObjectPool<T>(
        createFunc: () => Instantiate(_prefab),
        actionOnGet: (obj) => ActionOnGet(obj),
        actionOnRelease: (obj) => OnRelease(obj),
        maxSize: _poolMaxSize);
    }

    public virtual void ActionOnGet(T obj)
    {
        ObjectPoolChanged?.Invoke(obj, Pool);
        obj.Deactivation += ReturnToPool;
    }

    public virtual void ReturnToPool(T obj)
    {
        obj.Deactivation -= ReturnToPool;
        Pooling?.Invoke(obj);
        Pool.Release(obj);
    }

    public virtual void OnRelease(T obj)
    {
        obj.gameObject.SetActive(false);
        ObjectPoolChanged?.Invoke(obj, Pool);
    }
}
