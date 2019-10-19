using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ContextMenuControl : MonoBehaviour
{
    private Transform _owner;
    private Transform _colorTransform;
    private Transform _lightTransform;
    private Transform _textTransform;

    private void Start()
    {
        _owner = transform.parent;
        _colorTransform = transform.GetChild(1).transform;
        //_colorTransform.GetChild(0).GetChild(0).GetComponent<UIColorButton>()._editorUi = GameObject.FindWithTag("Editor_UI");
        _lightTransform = transform.GetChild(2).transform;
        _textTransform = transform.GetChild(3).transform;

        GetComponent<Canvas>().worldCamera = Camera.main;

        if (_owner != null)
        {
            if (_owner.GetComponent<MeshRenderer>() != null)
            {
                var renderer = _owner.GetComponent<MeshRenderer>();
                var color = _owner.GetComponent<MeshRenderer>().materials[0].color;
                _colorTransform.GetChild(0).GetChild(0).GetComponent<Image>().color = renderer.materials[0].color;

                //var shad = renderer.materials[0].shader; //= Shader.Find("Specular");
                //_lightTransform.GetChild(0).GetComponent<TextMeshProUGUI>().text = renderer.materials[0].GetFloat("_Metallic").ToString("F1");

                ////renderer.materials[0].shader = Shader.Find("Specular");
                //_lightTransform.GetChild(1).GetComponent<TextMeshProUGUI>().text = renderer.materials[0].GetFloat("_Glossiness").ToString("F1");
            }
                
        }
            

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
