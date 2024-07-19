using TMPro;
using UnityEngine;
using UnityEngine.Pool;

public class PoolCounterView<T> : MonoBehaviour where T : MonoBehaviour, IInitialized, IDeactivable<T>
{
    [SerializeField] private Spawner<T> _spawner;
    [SerializeField] private TMP_Text _count;

    private void OnEnable()
    {
        _spawner.ObjectPoolChanged += DisplayPool;
    }

    private void OnDisable()
    {
        _spawner.ObjectPoolChanged -= DisplayPool;
    }

    public virtual void DisplayPool(T obj, ObjectPool<T> pool)
    {
        _count.text = SetDisplayTemplate(obj, pool);
    }

    public virtual string SetDisplayTemplate(T obj, ObjectPool<T> pool)
    {
        return $"Всего {obj.name} в пуле {pool.CountAll}\n " +
            $"Всего активных на сцене {pool.CountActive}";
    }
}
