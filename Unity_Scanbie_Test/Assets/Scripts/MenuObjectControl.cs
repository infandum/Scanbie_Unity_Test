using System;
using System.Text;
using Assets.Scripts.Helpers;
using UnityEngine;
using UnityEngine.Rendering;

namespace Assets.Scripts
{
    public class MenuObjectControl : MonoBehaviour
    {
        [SerializeField] private GameObject _menuCanvas;
        private Transform _hoverMenu;
        private SelectionManager _selectionManager;
        private Bounds _bounds;

        //[SerializeField] private bool _overrideObject = false;
        public ObjectEditableData EditableData = new ObjectEditableData();
        public void OnHover()
        {
            Vector3 vecObjToCamera = Camera.main.transform.position - transform.position;
            vecObjToCamera = vecObjToCamera.normalized;
            //Vector3 offSetObjToCamera = Vector3.Scale(GetComponent<MenuObjectControl>().EditableData.MeshBounds.size, vecObjToCamera) /** (1 / avgScale)*/;
            Vector3 offSetObjToCamera = Vector3.Scale(_bounds.size, vecObjToCamera) /** (1 / avgScale)*/;
            _hoverMenu.transform.localPosition = offSetObjToCamera;

            _hoverMenu.gameObject.SetActive(true);
        }

        public void OnExit()
        {
            _hoverMenu.FindChildWithTag("Editor_UI").gameObject.SetActive(false);
            _hoverMenu.gameObject.SetActive(false);
        }

        private void Awake()
        {
            tag = "Editable";           

            if (EditableData.GUID.Equals("")) GetEditableDataFromObject();
            if (!_selectionManager)
                _selectionManager = FindObjectOfType<SelectionManager>();

            InstantiateHoverMenu();
        }

        private void InstantiateHoverMenu()
        {
            if (_menuCanvas == null)
                _menuCanvas = (GameObject)Resources.Load("HoverMenu");

            _hoverMenu = transform.FindChildWithTag("Hover_UI");
            if (_hoverMenu == null)
            {
                _hoverMenu = Instantiate(_menuCanvas, new Vector3(0, 0, 0), Quaternion.identity).transform;
                _hoverMenu.SetParent(transform, false);
            }

            float avgScale = (transform.localScale.x + transform.localScale.y + transform.localScale.z) / 3;
            var camTransform = Camera.main.transform;

            Vector3 vecObjToCamera = camTransform.position - transform.position;
            vecObjToCamera = vecObjToCamera.normalized;

            //Vector3 offSetObjToCamera = Vector3.Scale(EditableData.MeshBounds.size, vecObjToCamera);
            Vector3 offSetObjToCamera = Vector3.Scale(_bounds.size, vecObjToCamera);

            _hoverMenu.transform.localPosition = offSetObjToCamera;

            _hoverMenu.transform.localScale = (new Vector3(1.0f, 1.0f, 1.0f) / avgScale);

            _hoverMenu.gameObject.SetActive(false);
        }

        public void GetEditableDataFromObject()
        {
            EditableData.GUID = GetInstanceID().ToString();
       
            EditableData.Name = name;

            if (GetComponent<MeshFilter>() != null)
            {
                Mesh mesh = GetComponent<MeshFilter>().sharedMesh;
                //EditableData.MeshBounds = mesh.bounds;
                _bounds = mesh.bounds;
            }
            else if (GetComponent<Collider>() != null)
            {
                var col = GetComponent<Collider>();
                //EditableData.MeshBounds = col.bounds;
                _bounds = col.bounds;
            }

            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            if (meshRenderer)
            {
                EditableData.MainColor.SetColor(meshRenderer.sharedMaterials[0].color);
                EditableData.Metallic = meshRenderer.sharedMaterials[0].GetFloat("_Metallic");
                EditableData.Smoothness = meshRenderer.sharedMaterials[0].GetFloat("_Glossiness");
                EditableData.ShadowCasting = (int)meshRenderer.shadowCastingMode;
                EditableData.ReceiveShadow = meshRenderer.receiveShadows;
            }
        }

        public void SetEditableDataToObject()
        {
            name = EditableData.Name;

            //TODO: THINK ABOUT IO DATA MORE TOO PREVENT UNWANTED BREAKS/ERRORS/BUGS
            /*//if (GetComponent<MeshFilter>() != null)
            //{
            //    Mesh mesh = GetComponent<MeshFilter>().mesh;
            //    mesh.bounds = EditableData.MeshBounds;
            //}
            //else if (GetComponent<Collider>() != null)
            //{
            //    var col = GetComponent<Collider>();
            //    EditableData.MeshBounds = col.bounds;
            //}*/

            MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
            if (meshRenderer)
            {
                meshRenderer.sharedMaterials[0].color = EditableData.MainColor.ToColor();
                meshRenderer.sharedMaterials[0].SetFloat("_Metallic", EditableData.Metallic);
                meshRenderer.sharedMaterials[0].SetFloat("_Glossiness", EditableData.Smoothness);
                meshRenderer.shadowCastingMode = (ShadowCastingMode)EditableData.ShadowCasting;
                meshRenderer.receiveShadows = EditableData.ReceiveShadow;
            }

            if(_selectionManager)
                _selectionManager.SaveObjectsToFile();
        }
    }
}