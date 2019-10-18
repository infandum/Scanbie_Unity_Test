using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplayEffect : MonoBehaviour
{
    

    public Vector3 DegreesPerSecond = new Vector3(0.0f, 15.0f, 0.0f);
    public bool IsRotating = true;
    public bool ResetRotation = false;
    public float Amplitude = 0.5f;
    public float Frequency = 1f;


    private Vector3 _initPos;
    private Vector3 _initRot;
    void Start()
    {
        _initPos = transform.position;
        _initRot = transform.rotation.eulerAngles;
    }


    void LateUpdate()
    {
        if (IsRotating)
        {
            transform.Rotate(new Vector3(Time.deltaTime * DegreesPerSecond.x, Time.deltaTime * DegreesPerSecond.y, Time.deltaTime * DegreesPerSecond.z), Space.World);
        }
        else
        {
            if(ResetRotation)
            {
                transform.eulerAngles = _initRot;
            }
        }

        // Float up/down with a Sin()
        Vector3 _tempPos = _initPos;
        _tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * Frequency) * Amplitude;

        transform.position = _tempPos;
    }
}
