using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.MenuControllers
{
    public class ColorEditorUiControl : MonoBehaviour
    {
        //TODO CLEAN UP SWATCH STUFF
        private ColorSwatches _swatchManager;
        private Texture2D _texure;
        private Transform _swatch;
        private Transform _picker;

        private bool _usingPicker = true;

        private void OnEnable()
        {
            if (_picker == null)
                _picker = transform.GetChild(0);

            _picker.GetComponent<ColorPicker>().PreparePicker(transform.parent.parent.parent);


            if (_swatch == null)
                _swatch = transform.GetChild(1);
            if (_swatchManager == null)
                _swatchManager = GameObject.FindWithTag("GameController").GetComponent<ColorSwatches>();


            var texture = _swatchManager.Texture;
            if(_swatchManager.Texture)
                _swatch.GetChild(0).GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);

        }

        public void ChangeColor(Transform selected)
        {
            //TODO: CAUSES BUGGY INTERACTION WITH UI AND STUFF FIX EVENT MANAGEMENT
            Color color = Color.magenta;
            if (_usingPicker)
            {
                var pick = _picker.GetComponent<ColorPicker>();
                color = pick.GetColor();
            }
            else
            {
                if (_swatch.GetChild(0).GetComponent<RectTransform>())
                    color = _swatchManager.GetColor(_swatch.GetChild(0).GetComponent<RectTransform>());
            }


            //TODO: get multi mesh renderers
            if(selected.GetComponent<MeshRenderer>())
                selected.GetComponent<MeshRenderer>().materials[0].color = color;

            selected.FindChildWithTag("Hover_UI").GetComponent<HoverMenuControl>().Needupdate();
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
