﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private Material _defaultMaterial;
    [SerializeField] private Material _selectedMaterial;

    [SerializeField] private Transform _hover;
    [SerializeField] private Transform _selected;
    [SerializeField] private const string _targetTag = "Editable";

    [SerializeField] private Color _hoverColor = Color.yellow;
    [SerializeField] private Color _SelectColor = Color.green;
    [SerializeField] private float _lerpFactor = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_hover != null && _hover != _selected)
        {
            var hoverGlow = _hover.GetComponent<GlowObjectControl>();
            var hoverMenu = _hover.GetComponent<MenuObjectControl>();

            if (hoverGlow != null)
                hoverGlow.OnExit();
            if (hoverMenu != null)
                hoverMenu.OnExit();
            
            _hover = null;
        }


        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var target))
        {
            var targetTransform = target.transform;
            if (targetTransform.CompareTag(_targetTag))
            {
                var targetGlow = targetTransform.GetComponent<GlowObjectControl>();
                var targetMenu = targetTransform.GetComponent<MenuObjectControl>();
                if (targetGlow != null)
                {   
                    if (Input.GetMouseButtonDown(0))
                    {
                        if (_selected != null)
                        {
                            if(_selected.GetComponent<GlowObjectControl>() != null)
                                _selected.GetComponent<GlowObjectControl>().OnExit();
                            if(_selected.GetComponent<MenuObjectControl>() != null)
                                _selected.GetComponent<MenuObjectControl>().OnExit();                 
                        }
                        if (targetGlow != null)
                            targetGlow.OnSelect(_SelectColor);
                        _selected = targetTransform;
                    }
                    else
                    {
                        if (targetTransform != _selected)
                        {
                            if (targetGlow != null)
                                targetGlow.OnHover(_hoverColor);
                        }
                        if(targetMenu != null)
                            targetMenu.OnHover();
                       _hover = targetTransform;
                    }
                }
                
            }     
        }
    }
}
