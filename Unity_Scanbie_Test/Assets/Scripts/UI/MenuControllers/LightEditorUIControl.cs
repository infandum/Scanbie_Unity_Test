using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Assets.Scripts.UI.MenuControllers
{
    public class LightEditorUiControl : MonoBehaviour
    {
        
        private Transform _owner;
        private MenuObjectControl _menuControler;
        private Transform _metalTransform;
        private Transform _smoothTransform;
        private Transform _castTransform;
        private Transform _recieveTransform;

        private bool _needUpdate = false;
        // Start is called before the first frame update
        private void OnEnable()
        {
            if (!_owner)
                _owner = transform.parent.parent.parent;
            if (_menuControler == null)
            {
                _menuControler = _owner.GetComponent<MenuObjectControl>();
            }


            if (!_metalTransform)
            {
                _metalTransform = transform.GetChild(0);
                //_menuControler.EditableData.Metallic = _meshRenderer.materials[0].GetFloat("_Metallic");
            }


            if (!_smoothTransform)
            {
                _smoothTransform = transform.GetChild(1);
                //_menuControler.EditableData.Smoothness = _meshRenderer.materials[0].GetFloat("_Glossiness");
            }

            if (!_castTransform)
            {
                _castTransform = transform.GetChild(2);
                //_menuControler.EditableData.ShadowCasting = _meshRenderer.shadowCastingMode;
            }
                
            if (!_recieveTransform)
            {
                _recieveTransform = transform.GetChild(3);
                //_menuControler.EditableData.ReceiveShadow = _meshRenderer.receiveShadows;
            }
               

            UpdateOnce();
        }

        private void UpdateOnce()
        {      
            _metalTransform.GetComponent<Slider>().value = _menuControler.EditableData.Metallic;
            _metalTransform.GetChild(4).GetComponent<TextMeshProUGUI>().text = _menuControler.EditableData.Metallic.ToString("F");
        
            _smoothTransform.GetComponent<Slider>().value = _menuControler.EditableData.Smoothness;
            _smoothTransform.GetChild(4).GetComponent<TextMeshProUGUI>().text = _menuControler.EditableData.Smoothness.ToString("F");
         
            _castTransform.GetComponent<TMP_Dropdown>().value = (int)_menuControler.EditableData.ShadowCasting;
     
            _recieveTransform.GetComponent<Toggle>().isOn = _menuControler.EditableData.ReceiveShadow;
        }

        private void UpdateMeshRenderer()
        {
            if (_menuControler)
                _menuControler.SetEditableDataToObject();
        }

        public void NeedUpdate()
        {
            _needUpdate = true;
        }
        // Update is called once per frame
        private void Update()
        {
            if (!_needUpdate) return;
            UpdateOnce();

            _needUpdate = false;
        }

        public void UpdateMetal()
        {
            if (_metalTransform)
            {
                _menuControler.EditableData.Metallic = _metalTransform.GetComponent<Slider>().value;
                if (_metalTransform.GetChild(4))
                    _metalTransform.GetChild(4).GetComponent<TextMeshProUGUI>().text = _menuControler.EditableData.Metallic.ToString("F");
            }
               
            UpdateMeshRenderer();
        }

        public void UpdateSmoothness()
        {
            if (_smoothTransform)
            {
                _menuControler.EditableData.Smoothness = _smoothTransform.GetComponent<Slider>().value;
                if (_smoothTransform.GetChild(4))
                    _smoothTransform.GetChild(4).GetComponent<TextMeshProUGUI>().text = _menuControler.EditableData.Smoothness.ToString("F");
            }          
            UpdateMeshRenderer();
        }

        public void UpdateCastShadows()
        {
            if(_castTransform)
                _menuControler.EditableData.ShadowCasting = _castTransform.GetComponent<TMP_Dropdown>().value;
            UpdateMeshRenderer();
        }

        public void UpdateReceiveShadows()
        {
            if(_recieveTransform)
                _menuControler.EditableData.ReceiveShadow = _recieveTransform.GetComponent<Toggle>().isOn;
            UpdateMeshRenderer();
        }
    }
}
