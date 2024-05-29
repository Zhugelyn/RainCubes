using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class Cube : MonoBehaviour, IInitialized, IDeactivable<Cube>
{ 
    private float _hueMin = 0;
    private float _hueMax = 1;
    private int _minLifetime = 2;
    private int _maxLifetime = 5;
    private Color _defaultColor = Color.red;

    private bool _isDeactivated;
    private bool _isColorChanged;

    private MeshRenderer _meshRenderer;

    public event Action<Cube> Deactivation;

    public void Init()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _meshRenderer.material.color = _defaultColor;

        _isDeactivated = false;
        _isColorChanged = false;
    }

    public void ChangeColor()
    {
        if (_isColorChanged == false)
        {
            var color = UnityEngine.Random.ColorHSV(_hueMin, _hueMax);
            _meshRenderer.material.color = color;

            _isColorChanged = true;
        }
    }

    public void StartDeactivation()
    {
        if (_isDeactivated)
            return;

        _isDeactivated = true;

        StartCoroutine(Deactivate());
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
