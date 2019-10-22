/*
 * Rotates an object towards the currently active camera
 * 
 * 1. Attach CameraBillboard component to a canvas or a game object
 * 2. Specify the offset and you're done
 * 
 **/

using System;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    //public bool BillboardX = true;
    //public bool BillboardY = true;
    //public bool BillboardZ = true;
    public bool HasOffset = false;
    public float OffsetToCamera;
    private Vector3 _initLocalPos;
    private Transform _camera;

    void Start()
    {
        _camera = Camera.main.transform;
        _initLocalPos = transform.localPosition;
    }

    //Orient the camera after all movement is completed this frame to avoid jittering
    void LateUpdate()
    {
        transform.LookAt(transform.position + _camera.rotation * Vector3.forward, _camera.rotation * Vector3.up);
        transform.forward = _camera.forward;
        //if (!BillboardX || !BillboardY || !BillboardZ)
        //    transform.rotation = Quaternion.Euler(BillboardX ? transform.rotation.eulerAngles.x : 0f, BillboardY ? transform.rotation.eulerAngles.y : 0f, BillboardZ ? transform.rotation.eulerAngles.z : 0f);

        if (HasOffset || Math.Abs(OffsetToCamera) > 0.01f)
        {
            transform.localPosition = _initLocalPos;
            transform.position = transform.position + transform.rotation * Vector3.forward * OffsetToCamera;
        }

    }
}
