using Assets.Scripts.UI.MenuControllers;
using UnityEngine;

namespace Assets.Scripts.UI.Buttons
{
    public class UiTextInputfield : MonoBehaviour
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
        }


        public void OnDeselected()
        {
            transform.parent.GetComponent<TextEditorUiControl>().IsEditing(false);
        }
    }
}
