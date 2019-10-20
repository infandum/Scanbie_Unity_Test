﻿using System.Collections;
using System.Collections.Generic;
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
        if (_owner.GetComponent<MeshRenderer>() == null) return;
        var meshRenderer = _owner.GetComponent<MeshRenderer>();
        var color = _owner.GetComponent<MeshRenderer>().materials[0].color;
        _colorTransform.GetChild(0).GetChild(0).GetComponent<Image>().color = meshRenderer.materials[0].color;
    }

    public void Needupdate()
    {
        _needUpdate = true;
    }
    // Update is called once per frame
    void Update()
    {
        if(!_needUpdate) return;
        UpdateOnce();

        _needUpdate = false;
    }
}
