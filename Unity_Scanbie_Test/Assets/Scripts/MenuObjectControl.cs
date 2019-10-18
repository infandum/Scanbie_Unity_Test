using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuObjectControl : MonoBehaviour
{

    [SerializeField] private GameObject MenuCanvas;

    Transform HoverMenu;
    public void OnHover()
    {
        HoverMenu.gameObject.SetActive(true);
    }

    //public void OnSelect()
    //{
    //   
    //}

    public void OnExit()
    {
        HoverMenu.gameObject.SetActive(false);
    }

    void Start()
    {
        //MenuCanvas = (GameObject)Resources.Load("ContextMenu");
        //var menu = Instantiate(MenuCanvas, new Vector3(0, 0, 0), Quaternion.identity);
        //menu.transform.parent = transform;
    }

    void Awake()
    {
        if(MenuCanvas == null)
            MenuCanvas = (GameObject)Resources.Load("HoverMenu");

        
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).tag == "Hover_UI")
            {
                HoverMenu = transform.GetChild(i);
            }
        }

        if (HoverMenu == null)
        {
            HoverMenu = Instantiate(MenuCanvas, new Vector3(0, 0, 0), Quaternion.identity).transform;
            //HoverMenu.parent = transform;
            HoverMenu.SetParent(transform,false);
            Mesh mesh = GetComponent<MeshFilter>().mesh;
            Bounds bounds = mesh.bounds;
            HoverMenu.transform.localPosition = new Vector3(0, bounds.size.y, 0);        
        }

        HoverMenu.gameObject.SetActive(false);

    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
