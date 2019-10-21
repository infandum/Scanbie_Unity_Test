using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorPickerManager : MonoBehaviour
{
    public int textureWidth = 512;
    public int textureHeight = 512;

    [SerializeField] private Texture2D _colorTexture;
    [SerializeField] private Texture2D _saturationTexture;
    [SerializeField] private Texture2D _heuTexture;
    [SerializeField] private int _heu = 0;
    [SerializeField] private float _saturation = 1.0f;
    [SerializeField] private float _value = 1.0f;

    [SerializeField] private Texture2D styleTexture;

    public Texture2D GetColorTexture2D() { return _colorTexture; }
    public Texture2D GetSatuartionTexture2D() { return _saturationTexture; }
    public Texture2D GetHeuTexture2D() { return _heuTexture; }

    public int GetHeu() { return _heu; }
    void Awake()
    {
        GenerateColorTexture(_heu);

        GenerateSaturationTexture();

        GenerateHeuTexture();
    }

    private void GenerateHeuTexture()
    {
        _heuTexture = new Texture2D(textureWidth, textureHeight);
        for (int x = 0; x < _heuTexture.width; x++)
        {
            for (int y = 0; y < _heuTexture.height; y++)
            {
                var heuColor = new ColorHSV((x / (float) textureWidth) * 360, 1.0f, 1.0f);
                _heuTexture.SetPixel(x, y, heuColor.ToColor());
            }
        }

        _heuTexture.Apply();

        styleTexture = new Texture2D(1, 1);
        styleTexture.SetPixel(0, 0, new ColorHSV(_heu, _saturation, _value).ToColor());
    }

    private void GenerateSaturationTexture()
    {
        _saturationTexture = new Texture2D(textureWidth, textureHeight);
        for (int x = 0; x < _saturationTexture.width; x++)
        {
            for (int y = 0; y < _saturationTexture.height; y++)
            {
                float saturation = y / (float) _saturationTexture.height;
                var satColor = new Color(saturation, saturation, saturation);
                _saturationTexture.SetPixel(x, y, satColor);
            }
        }

        _saturationTexture.Apply();
    }

    public void GenerateColorTexture(int hue)
    {
        _heu = hue;
        _colorTexture = new Texture2D(textureWidth, textureHeight, TextureFormat.ARGB32, false);
        for (int x = 0; x < textureWidth; x++)
        {
            for (int y = 0; y < textureHeight; y++)
            {
                float sat = x / (float) textureWidth;
                float val = y / (float) textureHeight;
                var hsvColor = new ColorHSV(_heu, sat, val);
                _colorTexture.SetPixel(x, y, hsvColor.ToColor());
            }
        }

        _colorTexture.Apply();
    }
}
