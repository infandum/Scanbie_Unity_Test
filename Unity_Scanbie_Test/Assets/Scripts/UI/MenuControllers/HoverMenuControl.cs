using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HoverMenuControl : MonoBehaviour
{
    private Transform _owner;
    private Transform _colorTransform;
    private Transform _lightTransform;
    private Transform _textTransform;

    private bool _needUpdate = false;

    public Transform GetOwner()
    {
        return _owner;

    }

    private void Start()
    {
        _owner = transform.parent;
        _colorTransform = transform.GetChild(1).transform;
        //_lightTransform = transform.GetChild(2).transform;
        //_textTransform = transform.GetChild(3).transform;

        GetComponent<Canvas>().worldCamera = Camera.main;

        UpdateOnce();
    }

    private void UpdateOnce()
    {
        if (_owner == null) return;    
        if (_owner.GetComponent<MenuObjectControl>() == null) return;

        var objectControl = _owner.GetComponent<MenuObjectControl>();
        _owner.GetComponent<MenuObjectControl>().SetEditableDataToObject();

        var color = objectControl.EditableData.MainColor;
        _colorTransform.GetChild(0).GetChild(0).GetComponent<Image>().color = color.ToColor();
    }

    public void NeedUpdate()
    {
        if (_owner == null) return;
        if (_owner.GetComponent<MenuObjectControl>() == null) return;

        var objectControl = _owner.GetComponent<MenuObjectControl>();
        _owner.GetComponent<MenuObjectControl>().SetEditableDataToObject();

        var color = objectControl.EditableData.MainColor;
        _colorTransform.GetChild(0).GetChild(0).GetComponent<Image>().color = color.ToColor();
        //_needUpdate = true;
    }
    // Update is called once per frame
    private void Update()
    {
        if(!_needUpdate) return;
        UpdateOnce();

        _needUpdate = false;
    }
}
