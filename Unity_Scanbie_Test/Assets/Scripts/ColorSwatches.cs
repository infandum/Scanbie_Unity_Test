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

    public bool GenerateTexture = true;

    public List<Color> Swatches;

    public bool IsSaved = true;


    private int _swatchCountSqrt = 1;
    private int _swatchSize = 1;
    void Start()
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

    private void SaveTexture()
    {
        BasicIO.SaveTextureToPng(Texture, _textureName);
    }
}
