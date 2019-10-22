using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.UI;

public class ColorSwatches : MonoBehaviour
{

    public Texture2D Texture;
    public int TextureSize = 64;
    [SerializeField] private string _textureName = "Swatch";
    [SerializeField] private Color _currentColor;
    public bool GenerateTexture = true;

    public List<Color> Swatches;

    public bool IsSaved = true;


    private int _swatchCountSqrt = 1;
    private int _swatchSize = 1;
    void Awake()
    {
        if(!GenerateTexture) return;

        Texture = new Texture2D(TextureSize, TextureSize);
        Texture.name = "Swatch";


        _swatchCountSqrt = Mathf.CeilToInt(Mathf.Sqrt(Swatches.Count));
        if (_swatchCountSqrt % 2 != 0) _swatchCountSqrt++;
        _swatchSize = Texture.width / _swatchCountSqrt;

        int ySwatchIndex = 0;
        for (int y = Texture.height; y >= 0; y--)
        {
            if (y % _swatchSize == 0 && y < Texture.height && y > 0)
                ySwatchIndex++;
            var xSwatchIndex = 0;

            for (int x = 0; x < Texture.width; x++)
            {
                if (x % _swatchSize == 0 && x > 0)
                    xSwatchIndex++;

                var colorIndex = xSwatchIndex + (ySwatchIndex * _swatchCountSqrt);

                if (colorIndex < 0 )
                    print(colorIndex);
                Color color = colorIndex < Swatches.Count?  Swatches[colorIndex] : Color.white;
                Texture.SetPixel(x, y, color);
            }
        }
        Texture.Apply();

        if(IsSaved)
            SaveTexture();
    }

    public Color GetColor(Vector2 localPos)
    {
        //if (!textRectTransform) return Color.magenta;

        //RectTransformUtility.ScreenPointToLocalPointInRectangle(textRectTransform, Input.mousePosition, Camera.main, out var localPos);

        //print(localPos);
        //if (localPos.y < 0 || localPos.y >= 1) return Color.magenta;
        //if (localPos.x < 0 || localPos.x >= 1) return Color.magenta;

        return Texture.GetPixelBilinear(localPos.x, localPos.y);

    }

    public void PrepareSwatch(Transform swatch)
    {
        if(Texture)
            swatch.GetComponent<Image>().sprite = Sprite.Create(Texture, new Rect(0.0f, 0.0f, Texture.width, Texture.height), new Vector2(0.5f, 0.5f), 100.0f);
    }

    private void SaveTexture()
    {
        BasicIO.SaveTextureToPng(Texture, _textureName);
    }
}
