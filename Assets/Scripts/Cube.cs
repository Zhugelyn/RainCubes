using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Cube : MonoBehaviour, IInitialized
{ 
    private float _hueMin = 0;
    private float _hueMax = 1;
    private int _minLifetime = 2;
    private int _maxLifetime = 5;
    private bool _isDeactivated;
    private Color _defaultColor = Color.red;

    private SpawnerCube _spawner;

    public void Init(MonoBehaviour spawner)
    {
        GetComponent<MeshRenderer>().material.color = _defaultColor;

        _spawner = (SpawnerCube)spawner;
        _isDeactivated = false;
    }

    public void ChangeColor()
    {
        if (GetComponent<MeshRenderer>().material.color == _defaultColor)
        {
            var color = Random.ColorHSV(_hueMin, _hueMax);
            GetComponent<MeshRenderer>().material.color = color;
        }
    }

    public void StartDeactivationCountdown()
    {
        if (_isDeactivated)
            return;

        var lifetime = Random.Range(_minLifetime, _maxLifetime);
        Invoke("Deactivate", lifetime);
        _isDeactivated = true;
    }

    private void Deactivate()
    {
        _spawner.ReturnPool(this);
    }
}
