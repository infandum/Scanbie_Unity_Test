using System;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.MenuControllers
{
    public class TextEditorUiControl : MonoBehaviour
    {
        [SerializeField] private SelectionManager _selectionManager;

        [SerializeField]private string _textfileName = "LoremIpsum";
        [SerializeField] private string _noFile = "NoFile";
        private string _subfolder = "Resources";

        [SerializeField] private bool InUse = false;
        void OnEnable()
        {
            if(_selectionManager == null)
                _selectionManager = GameObject.FindWithTag("GameController").GetComponent<SelectionManager>();

            RefreshText();
            transform.GetChild(1).GetComponent<TMP_InputField>().text =  _textfileName + ".txt";
        }

        public void IsEditing(bool edit)
        {
            InUse = edit;
            _selectionManager.IsEditing(InUse);
        }

        public bool IsEditing()
        {
            return InUse;
        }

        public void LoadFile()
        {
            _textfileName = transform.GetChild(1).GetComponent<TMP_InputField>().text.Split('.')[0];
            RefreshText();
        }

        private void RefreshText()
        {
            var str = BasicIO.ReadFromFile(_textfileName) ?? BasicIO.ReadFromFile(_noFile);
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = str;
        }

        public void RefreshFile()
        {
        
        }
    }
}
