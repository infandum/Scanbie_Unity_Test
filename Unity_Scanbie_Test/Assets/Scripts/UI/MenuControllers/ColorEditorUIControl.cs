using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI.MenuControllers
{
    public class ColorEditorUiControl : MonoBehaviour
    {
        private ColorSwatches _swatchManager;
        private Texture2D _texure;
        private Transform _swatch;

        private void OnEnable()
        {
            if (_swatchManager == null)
                _swatchManager = GameObject.FindWithTag("GameController").GetComponent<ColorSwatches>();
            if (_swatch == null)
                _swatch = transform.GetChild(0).GetChild(0);

            _texure = _swatchManager.Texture;

            if(_texure)
                _swatch.GetComponent<Image>().sprite = Sprite.Create(_texure, new Rect(0.0f, 0.0f, _texure.width, _texure.height), new Vector2(0.5f, 0.5f), 100.0f);

        }

        public Color GetColor()
        {
            if (_swatch.GetComponent<RectTransform>())
            {
                Vector2 localPos;
                RectTransformUtility.ScreenPointToLocalPointInRectangle(_swatch.GetComponent<RectTransform>(), Input.mousePosition, Camera.main, out localPos);
                return _texure.GetPixelBilinear(localPos.x, localPos.y);
            }
        
            return Color.magenta;
        }

        public void ChangeColor(Transform selected)
        {
            var color = GetColor();
            //TODO: get multi mesh renderers
            if(selected.GetComponent<MeshRenderer>())
                selected.GetComponent<MeshRenderer>().materials[0].color = color;

            selected.FindChildWithTag("Hover_UI").GetComponent<HoverMenuControl>().Needupdate();
        }
    }
}
