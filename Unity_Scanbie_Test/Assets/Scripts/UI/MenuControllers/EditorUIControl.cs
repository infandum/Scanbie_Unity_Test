using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts;
using Assets.Scripts.UI.MenuControllers;

public class EditorUIControl : MonoBehaviour
{
    // Start is called before the first frame update
    public enum EditorControlOption
    {
        Color,
        Light,
        Text ,
        None    
    }

    private EditorControlOption _currentControlOption = EditorControlOption.None;

    public void OpenOptionMenu(EditorControlOption option)
    {
        _currentControlOption = option;
        gameObject.SetActive(true);

        switch (_currentControlOption)
        {
            case EditorControlOption.Color:
                if (transform.GetChild((int) _currentControlOption).gameObject.activeSelf)
                {
                    transform.DeactivateAllChideren();
                    break;
                }
                transform.DeactivateAllChideren();
                transform.GetChild((int)_currentControlOption).gameObject.SetActive(true);
                break;
            case EditorControlOption.Light:
                if (transform.GetChild((int)_currentControlOption).gameObject.activeSelf)
                {
                    transform.DeactivateAllChideren();
                    break;
                }
                transform.DeactivateAllChideren();
                transform.GetChild((int)_currentControlOption).gameObject.SetActive(true);
                break;
            case EditorControlOption.Text:
                if (transform.GetChild((int)_currentControlOption).gameObject.activeSelf)
                {
                    transform.DeactivateAllChideren();
                    break;
                }
                transform.DeactivateAllChideren();
                transform.GetChild((int)_currentControlOption).gameObject.SetActive(true);
                break;
            case EditorControlOption.None:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(_currentControlOption), _currentControlOption, null);
        }
    }


    public void ExecuteOption(Transform selected)
    {
        switch (_currentControlOption)
        {
            case EditorControlOption.Color:
                transform.GetChild((int)_currentControlOption).GetComponent<ColorEditorUiControl>().ChangeColor();

                break;
            case EditorControlOption.Light:
                break;
            case EditorControlOption.Text:
                break;
            case EditorControlOption.None:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(_currentControlOption), _currentControlOption, null);
        }
        
    }

    void Start()
    {
        //gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
}
