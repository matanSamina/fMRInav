using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sphereCorsur : MonoBehaviour
{
    public float vel = 4.0f;
    private Vector3 mouseI;
    // Update is called once per frame
    void Awake()
    {
        mouseI = Input.mousePosition;
    }

    void Update()
    {
        Vector3 mouse = Input.mousePosition;
        Vector3 dmouse = mouse - mouseI;
        transform.Rotate(new Vector3(0.0f, dmouse.y*vel, 0.0f) * Time.deltaTime);
        mouseI = Input.mousePosition;
    }
}
