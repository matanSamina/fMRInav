using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class menuExtras : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject mainMenu;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            mainMenu.GetComponent<menu>().onStagePress();
            mainMenu.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
