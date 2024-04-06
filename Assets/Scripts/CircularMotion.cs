using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircularMotion : MonoBehaviour
{
    float time;
    public float speed;
    public float height;
    public float width;
    public Transform center;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        float x = Mathf.Cos(time * speed) * width;
        float y = transform.position.y;
        float z = Mathf.Sin(time * speed) * width;

        transform.position = new Vector3(x, y, z);

        transform.LookAt(center);
    }
}
