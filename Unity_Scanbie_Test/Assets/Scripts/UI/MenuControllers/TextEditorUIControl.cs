using TMPro;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.UI.MenuControllers
{
    public class TextEditorUiControl : MonoBehaviour
    {
        private SelectionManager _selectionManager;
        private MenuObjectControl _menuControler;
        private string _noFile = "NoFile";
        private bool _inUse;

        private void OnEnable()
        {
            if(_selectionManager == null)
                _selectionManager = GameObject.FindWithTag("GameController").GetComponent<SelectionManager>();

            if (_menuControler == null)
                _menuControler = transform.parent.parent.parent.GetComponent<MenuObjectControl>();

            RefreshText();
            SetInputField();
        }

        private void SetInputField()
        {
            transform.GetChild(1).GetComponent<TMP_InputField>().text = _menuControler.Data.InfoFile + ".txt";
        }

        private string GetInputField()
        {
            return transform.GetChild(1).GetComponent<TMP_InputField>().text;
        }


        public void IsEditing(bool edit)
        {
            _inUse = edit;
            _selectionManager.IsEditing(_inUse);
        }

        public bool IsEditing()
        {
            return _inUse;
        }

        public void ReloadFile()
        {
            _menuControler.Data.InfoFile = transform.GetChild(1).GetComponent<TMP_InputField>().text.Split('.')[0];
            RefreshText();
        }

        public void SaveFile()
        {
            _menuControler.Data.InfoFile = transform.GetChild(1).GetComponent<TMP_InputField>().text.Split('.')[0];
            BasicIO.SaveToFile(_menuControler.Data.InfoFile, GetInputField());
            RefreshText();
        }

        public void UploadFile()
        {
            print(Application.dataPath);
            var path = EditorUtility.OpenFilePanel("Overwrite text file", Application.dataPath + "/Rescources/", "txt");
            if (!path.Equals(""))
            {
                var paths = path.Split('/');
                path = paths[paths.Length - 1];
                path = path.Split('.')[0];

                if (path != "")
                    _menuControler.Data.InfoFile = path;

                SetInputField();
                RefreshText();
            }
            
        }

        

        private void RefreshText()
        {
            var str = BasicIO.ReadFromFile(_menuControler.Data.InfoFile) ?? BasicIO.ReadFromFile(_noFile);
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = str;
        }
    }
}
