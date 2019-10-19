using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    private float moveSpeed = 0.5f;
    private float scrollSpeed = 10f;

    float horizontalInput;
    float verticalInput;
    float horizontalPanInput;
    float verticalPanInput;
    float wheelInput;

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalPanInput = Input.GetAxisRaw("Horizontal Pan");
        verticalPanInput = Input.GetAxisRaw("Vertical Pan");
        wheelInput = Input.GetAxis("Mouse ScrollWheel");
    }

    void FixedUpdate()
    {
        if (horizontalInput != 0 || verticalInput != 0)
        {
            transform.position += moveSpeed * new Vector3(horizontalInput, 0, verticalInput);
        }

        if (horizontalPanInput != 0 || verticalPanInput != 0)
        {
            transform.eulerAngles += moveSpeed * new Vector3(horizontalInput, 0, verticalInput);
        }

        if (wheelInput != 0)
        {
            transform.position += scrollSpeed * new Vector3(0, -Input.GetAxis("Mouse ScrollWheel"), 0);
        }
    }

}
