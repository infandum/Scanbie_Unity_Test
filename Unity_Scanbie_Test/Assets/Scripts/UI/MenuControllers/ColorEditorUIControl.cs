using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.MenuControllers
{
    public class ColorEditorUiControl : MonoBehaviour
    {
        //TODO: refractor swatches and picker
        private ColorSwatches _swatchManager;
        private Texture2D _texure;
        private Transform _swatch;
        private Transform _picker;
        private Transform _owner;

        private bool _usingPicker = true;
        private bool _switch = false;
        [SerializeField] private Color _currentColor;
        private void OnEnable()
        {
            if (_picker == null)
                _picker = transform.GetChild(0);
            if (_owner == null)
            {
                _owner = transform.parent.parent.parent;
            }
            else
            {
                _picker.GetComponent<ColorPicker>().PreparePicker(_owner);
                _currentColor = _owner.GetComponent<MeshRenderer>().materials[0].color;
            }
           

            if (_swatch == null)
                _swatch = transform.GetChild(1);
            if (_swatchManager == null)
                _swatchManager = GameObject.FindWithTag("GameController").GetComponent<ColorSwatches>();

            _swatchManager.PrepareSwatch(_swatch.GetChild(0));

        }

        public void ChangeColor(/*Transform selected*/)
        {
            if (_usingPicker)
            {
                var pick = _picker.GetComponent<ColorPicker>();
                _currentColor = pick.GetColor();
                
            }
            else
            {
                if (_swatch.GetChild(0).GetComponent<RectTransform>())
                {
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(_swatch.GetChild(0).GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out var localPos);
                    if (localPos.y < 0 || localPos.y >= 1 || localPos.x < 0 || localPos.x >= 1)
                        return;

                    _currentColor = _swatchManager.GetColor(localPos);
                }

            }
            UpdateOwner();
        }

        private void UpdateOwner()
        {
            //TODO: get multi mesh renderers
            if (_owner.GetComponent<MeshRenderer>())
                _owner.GetComponent<MeshRenderer>().materials[0].color = _currentColor;
            _owner.FindChildWithTag("Hover_UI").GetComponent<HoverMenuControl>().Needupdate();
        }

        public void SetCurrentColor(Color color)
        {
            _currentColor = color;
            UpdateOwner();
        }

        public void SwitchPicker()
        {
            _usingPicker = !_usingPicker;
            if (_usingPicker)
            {
                _swatch.gameObject.SetActive(false);
                _picker.gameObject.SetActive(true);
            }
            else
            {
                _swatch.gameObject.SetActive(true);
                _picker.gameObject.SetActive(false);
            }
        }
    }

}
