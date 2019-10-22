using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.Rendering;

[Serializable]
public class ObjectData
{
    public string Name = "Editable Object";
    public Bounds MeshBounds;
    public Color MainColor;
    public string InfoFile = "Lorem";

    public float Metalic;
    public float Smoothness;
    public ShadowCastingMode ShadowCasting = ShadowCastingMode.On;
    public bool RecieveShadow = true;
}

public class MenuObjectControl : MonoBehaviour
{

    [SerializeField] private GameObject MenuCanvas;

    private Transform _hoverMenu;

    [SerializeField] private bool OverrideObject = false;
    public ObjectData Data = new ObjectData();
    public void OnHover()
    {
            Vector3 vecObjToCamera = Camera.main.transform.position - transform.position;
            vecObjToCamera = vecObjToCamera.normalized;
            Vector3 offSetObjToCamera = Vector3.Scale(GetComponent<MenuObjectControl>().Data.MeshBounds.size, vecObjToCamera) /** (1 / avgScale)*/;
            _hoverMenu.transform.localPosition = offSetObjToCamera;

        _hoverMenu.gameObject.SetActive(true);
    }

    public void OnExit()
    {
        _hoverMenu.FindChildWithTag("Editor_UI").gameObject.SetActive(false);
        _hoverMenu.gameObject.SetActive(false);
    }

    void Awake()
    {
        tag = "Editable";
        Data.Name = name;

        if (MenuCanvas == null)
            MenuCanvas = (GameObject)Resources.Load("HoverMenu");

        
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "Hover_UI")
            {
                _hoverMenu = transform.GetChild(i);
            }
        }
        

        if (_hoverMenu == null)
        {
            _hoverMenu = Instantiate(MenuCanvas, new Vector3(0, 0, 0), Quaternion.identity).transform;
            _hoverMenu.SetParent(transform, false);      
        }

        if (GetComponent<MeshFilter>() != null)
        {
            Mesh mesh = GetComponent<MeshFilter>().mesh;
            Data.MeshBounds = mesh.bounds;
        }
        else if (GetComponent<Collider>() != null)
        {
            var col = GetComponent<Collider>();
            Data.MeshBounds = col.bounds;
        }

        float avgScale = (transform.localScale.x + transform.localScale.y + transform.localScale.z) / 3;
        var camTransform = Camera.main.transform;
        Vector3 vecObjToCamera = camTransform.position - transform.position;

        vecObjToCamera = vecObjToCamera.normalized;
        Vector3 offSetObjToCamera = Vector3.Scale(Data.MeshBounds.size, vecObjToCamera);
        _hoverMenu.transform.localPosition = offSetObjToCamera;

        _hoverMenu.transform.localScale = (new Vector3(1.0f, 1.0f, 1.0f) / avgScale);

        _hoverMenu.gameObject.SetActive(false);


    }
}
