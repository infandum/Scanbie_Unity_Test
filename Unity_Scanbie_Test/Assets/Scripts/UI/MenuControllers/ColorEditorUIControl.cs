using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.MenuControllers
{
    public class ColorEditorUiControl : MonoBehaviour
    {
        //TODO: refractor swatches and picker
        private Transform _owner;
        private MenuObjectControl _menuController;
        private ColorSwatches _swatchManager;
        private Texture2D _texture;
        private Transform _swatch;
        private Transform _picker;
       
        private bool _usingPicker = true;
        private void OnEnable()
        {
            if (_picker == null)
                _picker = transform.GetChild(0);
            if (_owner == null)
            {
                _owner = transform.parent.parent.parent;
                if (_menuController == null)
                {
                    _menuController = _owner.GetComponent<MenuObjectControl>();
                }
            }

            _picker.GetComponent<ColorPicker>().PreparePicker(_owner, _menuController.EditableData.MainColor.ToColor());

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
                _menuController.EditableData.MainColor.SetColor(pick.GetColor());
                
            }
            else
            {
                if (_swatch.GetChild(0).GetComponent<RectTransform>())
                {
                    RectTransformUtility.ScreenPointToLocalPointInRectangle(_swatch.GetChild(0).GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out var localPos);
                    if (localPos.y < 0 || localPos.y >= 1 || localPos.x < 0 || localPos.x >= 1)
                        return;

                    _menuController.EditableData.MainColor.SetColor(_swatchManager.GetColor(localPos));
                }

            }
            UpdateColor();
        }

        private void UpdateColor()
        {
            if (_menuController)
                _menuController.SetEditableDataToObject();
            _owner.FindChildWithTag("Hover_UI").GetComponent<HoverMenuControl>().NeedUpdate();
        }

        public void SetCurrentColor(Color color)
        {
            _menuController.EditableData.MainColor.SetColor(color);
            UpdateColor();
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
