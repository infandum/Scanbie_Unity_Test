using System.Globalization;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

namespace Assets.Scripts.UI.MenuControllers
{
    public class LightEditorUiControl : MonoBehaviour
    {
        private MenuObjectControl _menuControler;
        private Transform _owner;
        private MeshRenderer _meshRenderer;
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
                _meshRenderer = _owner.GetComponent<MeshRenderer>();
            }


            if (!_metalTransform)
            {
                _metalTransform = transform.GetChild(0);
                _menuControler.Data.Metalic = _meshRenderer.materials[0].GetFloat("_Metallic");
            }


            if (!_smoothTransform)
            {
                _smoothTransform = transform.GetChild(1);
                _menuControler.Data.Smoothness = _meshRenderer.materials[0].GetFloat("_Glossiness");
            }

            if (!_castTransform)
            {
                _castTransform = transform.GetChild(2);
                _menuControler.Data.ShadowCasting = _meshRenderer.shadowCastingMode;
            }
                
            if (!_recieveTransform)
            {
                _recieveTransform = transform.GetChild(3);
                _menuControler.Data.RecieveShadow = _meshRenderer.receiveShadows;
            }
               

            UpdateOnce();
        }

        private void UpdateOnce()
        {      
            _metalTransform.GetComponent<Slider>().value = _menuControler.Data.Metalic;
            _metalTransform.GetChild(4).GetComponent<TextMeshProUGUI>().text = _menuControler.Data.Metalic.ToString("F");
        
            _smoothTransform.GetComponent<Slider>().value = _menuControler.Data.Smoothness;
            _smoothTransform.GetChild(4).GetComponent<TextMeshProUGUI>().text = _menuControler.Data.Smoothness.ToString("F");
         
            _castTransform.GetComponent<TMP_Dropdown>().value = (int)_menuControler.Data.ShadowCasting;
     
            _recieveTransform.GetComponent<Toggle>().isOn = _menuControler.Data.RecieveShadow;
        }

        private void UpdateMeshRenderer()
        {
            if(!_meshRenderer) return;
            _meshRenderer.materials[0].SetFloat("_Metallic", _menuControler.Data.Metalic);
            _meshRenderer.materials[0].SetFloat("_Glossiness", _menuControler.Data.Smoothness);
            _meshRenderer.shadowCastingMode = _menuControler.Data.ShadowCasting;
            _meshRenderer.receiveShadows = _menuControler.Data.RecieveShadow;
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
                _menuControler.Data.Metalic = _metalTransform.GetComponent<Slider>().value;
                if (_metalTransform.GetChild(4))
                    _metalTransform.GetChild(4).GetComponent<TextMeshProUGUI>().text = _menuControler.Data.Metalic.ToString("F");
            }
               
            UpdateMeshRenderer();
        }

        public void UpdateSmoothness()
        {
            if (_smoothTransform)
            {
                _menuControler.Data.Smoothness = _smoothTransform.GetComponent<Slider>().value;
                if (_smoothTransform.GetChild(4))
                    _smoothTransform.GetChild(4).GetComponent<TextMeshProUGUI>().text = _menuControler.Data.Smoothness.ToString("F");
            }          
            UpdateMeshRenderer();
        }

        public void UpdateCastShadows()
        {
            if(_castTransform)
                _menuControler.Data.ShadowCasting = (ShadowCastingMode)_castTransform.GetComponent<TMP_Dropdown>().value;
            UpdateMeshRenderer();
        }

        public void UpdateRecieveShadows()
        {
            if(_recieveTransform)
                _menuControler.Data.RecieveShadow = _recieveTransform.GetComponent<Toggle>().isOn;
            UpdateMeshRenderer();
        }
    }
}
