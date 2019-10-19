using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuObjectControl : MonoBehaviour
{

    [SerializeField] private GameObject MenuCanvas;

    private Transform _hoverMenu;
    private Bounds _bounds;

    public Bounds Bounds { get => _bounds;}

    public void OnHover()
    {
            Vector3 vecObjToCamera = Camera.main.transform.position - transform.position;
            vecObjToCamera = vecObjToCamera.normalized;
            Vector3 offSetObjToCamera = Vector3.Scale(GetComponent<MenuObjectControl>().Bounds.size, vecObjToCamera) /** (1 / avgScale)*/;
            _hoverMenu.transform.localPosition = offSetObjToCamera;

        _hoverMenu.gameObject.SetActive(true);
    }

    //public void OnSelect()
    //{
    //   
    //}

    public void OnExit()
    {
        _hoverMenu.gameObject.SetActive(false);
    }

    void Start()
    {

    }

    void Awake()
    {
        tag = "Editable";

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
            _bounds = mesh.bounds;
        }
        else if (GetComponent<Collider>() != null)
        {
            var col = GetComponent<Collider>();
            _bounds = col.bounds;
        }

        float avgScale = (transform.localScale.x + transform.localScale.y + transform.localScale.z) / 3;



        var camTransform = Camera.main.transform;
        Vector3 vecObjToCamera = Camera.main.transform.position - transform.position;
        //vecObjToCamera = vecObjToCamera + new Vector3(0.1f, 0.1f, 0.1f);
        vecObjToCamera = vecObjToCamera.normalized;
        Vector3 offSetObjToCamera = Vector3.Scale(Bounds.size, vecObjToCamera) /** (1 / avgScale)*/;
        print(name + ":: " + Bounds.size.y + " // Scale:: " + avgScale + " // Bounds.y / Scale:: " + (Bounds.size.y / avgScale));
        print(name + "-Direction:: " + vecObjToCamera + " // Offset:: " + offSetObjToCamera);
        _hoverMenu.transform.localPosition = offSetObjToCamera;


        //_hoverMenu.transform.position = new Vector3(transform.position.x , transform.position.y + _bounds.size.y /*/ (_bounds.size.y * avgScale)*/, transform.position.z/* - _bounds.size.z*/);
        
        _hoverMenu.transform.localScale = /*new Vector3(1.0f, 1.0f, 1.0f) +*/ (new Vector3(1.0f, 1.0f, 1.0f) / avgScale);

        _hoverMenu.gameObject.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
