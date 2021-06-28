using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateObj : MonoBehaviour
{
    public float RotateVel = 1;
    public bool IsPressed = false;
    public float rotatemulti = 1;
    // Update is called once per frame
    void Update()
    {
        if (IsPressed) { RotateVel = RotateVel * 1.06f; }
        transform.Rotate(new Vector3(0f, 2f*RotateVel* rotatemulti, 0f) * Time.deltaTime);
    }


}
