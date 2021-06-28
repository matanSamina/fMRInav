using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Dest", 2.0f);
    }

    // Update is called once per frame
    private void Dest()
    {
        Destroy(gameObject);
    }
}
