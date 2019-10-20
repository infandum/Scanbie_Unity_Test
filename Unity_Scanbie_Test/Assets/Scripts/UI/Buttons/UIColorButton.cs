using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;

public class UIColorButton : MonoBehaviour
{
    private Transform _editorUi;
    void Awake()
    {
        _editorUi = transform.parent.parent.FindChildWithTag("Editor_UI");
    }
    public void OpenEditorUi()
    {
        if(_editorUi == null)
            _editorUi = transform.parent.parent.FindChildWithTag("Editor_UI");
        //_editorUi.gameObject.SetActive(true);
        _editorUi.GetComponent<EditorUIControl>().OpenOptionMenu(EditorUIControl.EditorControlOption.Color);
        
    }
}
