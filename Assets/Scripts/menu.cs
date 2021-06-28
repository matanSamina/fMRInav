using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;


public class menu : MonoBehaviour
{
    private int LvlToLoad;
    public float Accelration;
    public GameObject Sphere;
    private rotateObj rotate;


    void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        rotate = Sphere.GetComponent<rotateObj>();

    }

    public void OnClick(int Lvl)
    {
        LvlToLoad = Lvl;
        rotate.IsPressed = true;
        Invoke("LoadNextLevel", 4.0f);
    }

    private void LoadNextLevel()
    {

        SceneManager.LoadScene(LvlToLoad);
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape key was pressed");
            closeGame();
        }

    }

    private void closeGame()
    {
        Application.Quit();
    }

    public void onStagePress()
    {
        rotate.rotatemulti = 50;
        Invoke("regSphere", 0.5f);
    }

    private void regSphere()
    {
        rotate.rotatemulti = 1;
    }
}
