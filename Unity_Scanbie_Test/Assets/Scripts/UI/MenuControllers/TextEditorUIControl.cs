using TMPro;
using UnityEngine;

namespace Assets.Scripts.UI.MenuControllers
{
    public class TextEditorUiControl : MonoBehaviour
    {
        private SelectionManager _selectionManager;
        [SerializeField]private string _textfileName = "LoremIpsum";
        private string _noFile = "NoFile";
        private bool _inUse;

        private void OnEnable()
        {
            if(_selectionManager == null)
                _selectionManager = GameObject.FindWithTag("GameController").GetComponent<SelectionManager>();

            RefreshText();
            transform.GetChild(1).GetComponent<TMP_InputField>().text =  _textfileName + ".txt";
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
    }
}
