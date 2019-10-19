using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIColorButton : MonoBehaviour
{
    public GameObject _editorUi;
    void Awake()
    {
        _editorUi = GameObject.FindGameObjectsWithTag("Editor_UI")[0];
        return;
        print(_editorUi);
    }
    public void OpenEditorUi()
    {
        _editorUi.gameObject.SetActive(true);
    }
}
