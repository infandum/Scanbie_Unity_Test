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
        public void Reload()
        {
            transform.parent.GetComponent<TextEditorUiControl>().ReloadFile();  
        }

        public void Save()
        {
            transform.parent.GetComponent<TextEditorUiControl>().ReloadFile();
        }

        public void Upload()
        {
            transform.parent.GetComponent<TextEditorUiControl>().ReloadFile();
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
