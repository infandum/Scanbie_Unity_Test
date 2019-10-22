using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SelectionManager : MonoBehaviour
{
    [SerializeField] private Transform _hover;
    [SerializeField] private Transform _selected;
    [SerializeField] private const string _targetTag = "Editable";

    [SerializeField] private Color _hoverColor = Color.yellow;
    [SerializeField] private Color _SelectColor = Color.green;
    [SerializeField] private float _lerpFactor = 10; 

    private bool _isBusyInUi = false;
    private bool _hideUI = false;
    private Camera _mainCamera;

    public float GetLerpFactor() { return _lerpFactor;}
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = Camera.main;
    }

    private void DeselectObject()
    {
        if (_selected != null)
        {
            if (_selected.GetComponent<GlowObjectControl>() != null)
                _selected.GetComponent<GlowObjectControl>().OnExit();
            if (_selected.GetComponent<MenuObjectControl>() != null)
                _selected.GetComponent<MenuObjectControl>().OnExit();
        }
        _selected = null;
    }


    public void IsEditing(bool edit)
    {
        _isBusyInUi = edit;
        _mainCamera.GetComponent<CameraController>().Active = !_isBusyInUi;
    }

    public bool IsEditing()
    {
        return _isBusyInUi;
    }

    private void ToggleUi()
    {
        if (Input.GetKeyDown(KeyCode.V)) _hideUI = !_hideUI;

        if (_hideUI)
        {
            if (_hover != null)
                if (_hover.GetComponent<MenuObjectControl>() != null)
                    _hover.GetComponent<MenuObjectControl>().OnExit();

            if (_selected != null)
                if (_selected.GetComponent<MenuObjectControl>() != null)
                    _selected.GetComponent<MenuObjectControl>().OnExit();
        }
        else
        {
            if (_selected != null)
                if (_selected.GetComponent<MenuObjectControl>() != null)
                    _selected.GetComponent<MenuObjectControl>().OnHover();
        }
           
    }
    void Update()
    {
        ToggleUi();

        //if you want to keep the hover menu open on the selected object change:
        //_hover != _selected
        if (_hover != null && _hover != _selected)
        {
            if (!_isBusyInUi)
            {
                var hoverGlow = _hover.GetComponent<GlowObjectControl>();
                var hoverMenu = _hover.GetComponent<MenuObjectControl>();

                if (hoverGlow != null /*&& _hover != _selected*/)
                    hoverGlow.OnExit();
                if (hoverMenu != null)
                    hoverMenu.OnExit();

                _hover = null;
            }          
        }


        var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var target))
        {
            var targetTransform = target.transform;

            if (targetTransform.CompareTag("Hover_UI"))
            {
                _hover = targetTransform;
                if (Input.GetMouseButtonDown(0))
                {
                    if (_hover.transform.parent)
                    {
                        var editorUi = _hover.transform.parent;
                        if(editorUi.GetComponent<EditorUIControl>())
                            editorUi.GetComponent<EditorUIControl>().ExecuteOption(_selected);
                    }
                }
                return;
            }
               
            if(_isBusyInUi) return;

            if (targetTransform.CompareTag(_targetTag))
            {
                var targetGlow = targetTransform.GetComponent<GlowObjectControl>();
                var targetMenu = targetTransform.GetComponent<MenuObjectControl>();
                    if (Input.GetMouseButtonDown(0))
                    {
                        DeselectObject();

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
                        if(targetMenu != null && !_hideUI)
                            targetMenu.OnHover();
                       _hover = targetTransform;
                    }   
            }
            else
            {
                if (Input.GetMouseButtonDown(0))
                {
                    DeselectObject();
                }
            }     
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                DeselectObject();
            }
        }
    }
}
