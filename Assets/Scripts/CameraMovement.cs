using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] Transform center;
    [SerializeField] float height;
    [SerializeField] float width;
    float x, y, z;
    float movement = 0;
    // Start is called before the first frame update
    void Start()
    {
        RotateCamera();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.RightArrow))
        {
            movement += rotationSpeed * Time.deltaTime;
            RotateCamera();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            movement -= rotationSpeed * Time.deltaTime;
            RotateCamera();
        }
        transform.LookAt(center);
    }

    void RotateCamera()
    {
        x = Mathf.Cos(movement) * width;
        y = height;
        z = Mathf.Sin(movement) * width;
        transform.position = new Vector3(x, y, z);
    }
}
