using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplayEffect : MonoBehaviour
{
    

    public Vector3 DegreesPerSecond = new Vector3(0.0f, 15.0f, 0.0f);
    public bool IsRotating = false;
    public bool ResetRotation = false;
    public float Amplitude = 0.5f;
    public float Frequency = 1f;
    public bool IsHovering = false;
    public bool ResetPosition = false;

    public Vector3 InitPos;
    public Vector3 InitRot;
    void Start()
    {
        InitPos = transform.position;
        InitRot = transform.rotation.eulerAngles;
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
                transform.eulerAngles = InitRot;
            }
        }


        if (IsHovering)
        {
            // Float up/down with a Sin()
            Vector3 tempPos = InitPos;
            tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * Frequency) * Amplitude;

            transform.position = tempPos;
        }
        else
        {
            if (ResetPosition)
            {
                transform.position = InitPos;
            }
        }
        
    }
}
