using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUBControl : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private float _countDown = 10.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) _countDown = -1;

        _countDown -= Time.deltaTime;
        if (_countDown < 0)
        {
           gameObject.SetActive(false);
        }
    }
}
