using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.IMGUI.Controls;
using UnityEngine;
using Assets.Scripts;

public class TextEditorUIControl : MonoBehaviour
{

    [SerializeField]private string _textfileName = "LoremIpsum";
    private string _subfolder = "Resources";

    void OnEnable()
    {
        var str = BasicIO.ReadFromFile(_textfileName);
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = str;
        transform.GetChild(1).GetComponent<TMP_InputField>().text = _subfolder + "/" + _textfileName + ".txt";
    }

    public void UploadFile()
    {

    }
    public void RefreshFile()
    {
        
    }
}
