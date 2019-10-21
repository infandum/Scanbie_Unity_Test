using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.UI.MenuControllers;
using UnityEngine;

public class UITextInputfield : MonoBehaviour
{
    //public void OnSave()
    //{
    //    transform.parent.GetComponent<TextEditorUiControl>().IsEditing(true);
    //    print("Select");
    //}
    public void Onload()
    {
        transform.parent.GetComponent<TextEditorUiControl>().LoadFile();
        
    }

    public void OnSelected()
    {
        transform.parent.GetComponent<TextEditorUiControl>().IsEditing(true);
        print("Select");
    }


    public void OnDeselected()
    {
        transform.parent.GetComponent<TextEditorUiControl>().IsEditing(false);
        print("Deselect");
    }
}
