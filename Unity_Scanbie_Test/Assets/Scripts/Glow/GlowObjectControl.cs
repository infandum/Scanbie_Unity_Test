using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowObjectControl : MonoBehaviour
{
    [SerializeField] private float _lerpFactor = 10;

    public bool IsSelected = false;

    public Renderer[] Renderers
    {
        get;
        private set;
    }

    public Color CurrentColor
    {
        get { return _currentColor; }
    }

    private Color _currentColor;
    private Color _targetColor;

    public void OnHover(Color color)
    {
        _targetColor = color;
        enabled = true;
    }

    public void OnSelect(Color color)
    {
        _targetColor = color;
        enabled = true;
        IsSelected = true;
    }

    public void OnExit()
    {
        _targetColor = Color.black;
        enabled = true;
        IsSelected = false;
    }

    void Start()
    {
        Renderers = GetComponentsInChildren<Renderer>();
        GlowCameraController.RegisterObject(this);

        tag = "Editable";
    }

    private void Update()
    {
        _currentColor = Color.Lerp(_currentColor, _targetColor, Time.deltaTime * _lerpFactor);

        if (_currentColor.Equals(_targetColor))
        {
            enabled = false;
        }
    }
}
