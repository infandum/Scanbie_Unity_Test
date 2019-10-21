using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using Assets.Scripts.UI.MenuControllers;
using UnityEngine;
using UnityEngine.UI;

public class ColorPicker : MonoBehaviour
{
    private Transform _owner;
    private ColorPickerManager _pickManager;
    [SerializeField] private Color _currentColor;
    private Transform _picker;
    private Texture2D _colorTexture2D;

    public int Hue;
    private Slider _HueSlider;
    public float Value;

    private Slider _valueSlider;
    // Start is called before the first frame update
    void OnEnable()
    {
        
    }

    public void PreparePicker(Transform owner)
    {
        //TODO: CLEAN THIS UP AND FIX EVENT MANAGEMENT
        _owner = owner;
        _picker = transform.GetChild(2).GetChild(0);
        _HueSlider = transform.GetChild(0).GetComponent<Slider>();
        _valueSlider = transform.GetChild(1).GetComponent<Slider>();
        _pickManager = GameObject.FindWithTag("GameController").GetComponent<ColorPickerManager>();

        var meshRenderer = _owner.GetComponent<MeshRenderer>();

        _currentColor = meshRenderer.materials[0].color;
        ColorHSV color = new ColorHSV(_currentColor);

        _HueSlider.value = color.h;
        Hue = (int)_HueSlider.value;

        _valueSlider.value = color.v;
        Value = (int)_valueSlider.value;

        _pickManager.GenerateColorTexture(Hue);

        _picker.localPosition = new Vector3(color.s, color.v);
        _picker.GetChild(0).GetComponent<Image>().color = _currentColor;

        var texture = _pickManager.GetHeuTexture2D();
        transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);

        texture = _pickManager.GetSatuartionTexture2D();
        transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = Sprite.Create(texture, new Rect(0.0f, 0.0f, texture.width, texture.height), new Vector2(0.5f, 0.5f), 100.0f);

        _colorTexture2D = _pickManager.GetColorTexture2D();
        transform.GetChild(2).GetComponent<Image>().sprite = Sprite.Create(_colorTexture2D, new Rect(0.0f, 0.0f, _colorTexture2D.width, _colorTexture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
    }

    public void SetHeu(Slider slider)
    {
        _HueSlider = slider;
        Hue = (int)slider.value;

        _pickManager.GenerateColorTexture(Hue);

        _colorTexture2D = _pickManager.GetColorTexture2D();
        transform.GetChild(2).GetComponent<Image>().sprite = Sprite.Create(_colorTexture2D, new Rect(0.0f, 0.0f, _colorTexture2D.width, _colorTexture2D.height), new Vector2(0.5f, 0.5f), 100.0f);
        var x = Mathf.Clamp(_picker.localPosition.x, 0, 0.99f);
        var y = Mathf.Clamp(_picker.localPosition.y, 0, 0.99f);
        _currentColor = _colorTexture2D.GetPixelBilinear(x, y);
        _picker.GetChild(0).GetComponent<Image>().color = _currentColor;
    }
    public void SetValue(Slider slider)
    {
        _valueSlider = slider;
        Value = _valueSlider.value;

        _picker.localPosition = new Vector3(_picker.localPosition.x, Value, _picker.localPosition.z);
        _currentColor = _colorTexture2D.GetPixelBilinear(_picker.localPosition.x, _picker.localPosition.y);

        _picker.GetChild(0).GetComponent<Image>().color = _currentColor;
    }

    public void PickColor(RectTransform textRectTransform)
    {
        if (!textRectTransform) return;
        Vector2 localPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(textRectTransform, Input.mousePosition, Camera.main, out localPos);
        localPos.x = Mathf.Clamp(localPos.x, 0, 0.99f);
        localPos.y = Mathf.Clamp(localPos.y, 0, 0.99f);
        _currentColor = _colorTexture2D.GetPixelBilinear(localPos.x, localPos.y);
        _picker.localPosition = localPos;
        _picker.GetChild(0).GetComponent<Image>().color = _currentColor;

        _valueSlider.value = new ColorHSV(_currentColor).v;
    }

    public Color GetColor()
    {
        return _currentColor;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
