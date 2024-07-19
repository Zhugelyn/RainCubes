using UnityEngine;

public class SpawnerBomb : Spawner<Bomb>
{
    [SerializeField] private Spawner<Cube> _spawner;

    private void OnEnable()
    {
        _spawner.Pooling += CreateBomb;
    }

    private void OnDisable()
    {
        _spawner.Pooling -= CreateBomb;
    }

    private void CreateBomb(Cube cube)
    {
        var bomb = Pool.Get();
        bomb.Init(cube.transform.position);
        cube.gameObject.SetActive(true);
    }
}
