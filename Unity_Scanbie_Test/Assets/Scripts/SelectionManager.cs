using System.Collections.Generic;
using Assets.Scripts.Helpers;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts
{
    public class SelectionManager : MonoBehaviour
    {
        [SerializeField] private /*readonly*/ Color _hoverColor = Color.white;
        [SerializeField] private Transform _hover;
        [SerializeField] private /*readonly*/ Color _selectColor = Color.green;  
        [SerializeField] private Transform _selected;

        private Camera _mainCamera;

        private const float LerpFactor = 10.0f;
        private const string TargetTag = "Editable";
        private bool _isBusyInUi;
        private bool _hideUi;

        public string FileName = "EditableObjects";

        [SerializeField] private List<Transform> _editableObjects = new List<Transform>();


        public float GetLerpFactor() { return LerpFactor;}
        // Start is called before the first frame update
        private void Start()
        {
            if (!_mainCamera)
                _mainCamera = Camera.main;

            LoadObjectsFromFile();
        }

        private void Update()
        {
            ToggleUi();
            ExitHover();
            OnHover();
        }

        private void OnHover()
        {
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
                            if (editorUi.GetComponent<EditorUIControl>())
                                editorUi.GetComponent<EditorUIControl>().ExecuteOption(_selected);
                        }
                    }

                    return;
                }

                if (_isBusyInUi) return;

                if (targetTransform.CompareTag(TargetTag))
                {
                    var targetGlow = targetTransform.GetComponent<GlowObjectControl>();
                    var targetMenu = targetTransform.GetComponent<MenuObjectControl>();
                    if (Input.GetMouseButtonDown(0))
                    {
                        DeselectObject();

                        if (targetGlow != null)
                            targetGlow.OnSelect(_selectColor);
                        _selected = targetTransform;
                    }
                    else
                    {
                        if (targetTransform != _selected)
                        {
                            if (targetGlow != null)
                                targetGlow.OnHover(_hoverColor);
                        }

                        if (targetMenu != null && !_hideUi)
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

        private void ExitHover()
        {
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

        public void GetAllEditableObjects()
        {
            _editableObjects.Clear();
            foreach (var obj in FindObjectsOfType<MenuObjectControl>())
            {
                _editableObjects.Add(obj.transform);
            }
        }

        public void SaveObjectsToFile()
        {
            //TODO: Mesh bounds give recursive error on JsonConvert but no on JsonUtilities
            if(_editableObjects.Count <= 0) return;
            List<ObjectEditableData> objData = new List<ObjectEditableData>();
            List<string> data = new List<string>();
            foreach (var obj in _editableObjects)
            {
                objData.Add(obj.GetComponent<MenuObjectControl>().EditableData);
            }

            var json = JsonConvert.SerializeObject(objData.ToArray());
            BasicIO.SaveToFile(json, FileName, ".json");
        }

        public void LoadObjectsFromFile()
        {
           var json = BasicIO.ReadFromFile(FileName, ".json");
           if (json.Equals("")) return;
           var deserialize = JsonConvert.DeserializeObject<List<ObjectEditableData>>(json);

            foreach (var data in deserialize)
            {
                foreach (var editObj in _editableObjects)
                {
                    var obj = editObj.GetComponent<MenuObjectControl>();
                    if (obj.EditableData.GUID.Equals(data.GUID))
                    {
                        obj.EditableData = data;
                        obj.SetEditableDataToObject();
                        break;
                    }
                }
            }
           
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
            if (Input.GetKeyDown(KeyCode.V)) _hideUi = !_hideUi;

            if (_hideUi)
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

    }
}
