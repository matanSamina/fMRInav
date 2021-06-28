using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.IO;
using System.IO;
using System.Security.AccessControl;
using UnityEngine.SceneManagement;
using System;



public class logSys : MonoBehaviour
{
    // Start is called before the first frame update
    private string logName;
    [SerializeField] private GameObject player;
    private int Time;

    void Awake()
    {
        player = GameObject.FindWithTag("Player"); // perent of tube 1 segments


        Directory.CreateDirectory(Application.streamingAssetsPath + "/Position_Folder/");
        Scene scene = SceneManager.GetActiveScene();
        Time = (int)(System.DateTime.UtcNow - new System.DateTime(1970, 1, 1)).TotalSeconds;
        logName = Application.streamingAssetsPath + "/Position_Folder/" + scene.name +"atTime"+ Time.ToString() +".txt";

        File.WriteAllText(logName, scene.name + "\n");

        Debug.Log("time in unix" + Time.ToString());

        File.AppendAllText(logName, "starting time in Unix:" + Time.ToString() + "\n");

    }

    // Update is called once per frame
    void Update()
    {
        int currentTime = (int)(System.DateTime.UtcNow - new System.DateTime(1970, 1, 1)).TotalMilliseconds - Time;

        string posx = player.transform.position.x.ToString();
        string posz = player.transform.position.z.ToString();

        string rot = player.transform.rotation.y.ToString();
        string poseData = "t" +","+ currentTime.ToString() +"," + "x" + "," + posx + "," + "z" + "," + posz + "," + "rot" + "," + rot; 
        //Debug.Log(poseData);
        File.AppendAllText(logName, poseData + "\n");
        //Debug.Log(milliseconds.ToString());

    }

    public void updateEventLog(int num , string EventName)
    {
        int currentTime = (int)(System.DateTime.UtcNow - new System.DateTime(1970, 1, 1)).TotalSeconds - Time;
        File.AppendAllText(logName , EventName + ","+ "t" + "," + currentTime.ToString() + "," + num.ToString() + "\n");
        Debug.Log("Coin to Log");
    }
}
