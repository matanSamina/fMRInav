using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class coinsS : MonoBehaviour
{

    [HideInInspector] public int score = 0;
    [HideInInspector] public int counter = 0;
    public Coin coin;
    [HideInInspector] public int NumOfCoins;

    public TextAsset poseListCSV; // Reference of CSV file
    private char lineSeperater = '\n'; // It defines line seperate character
    private char fieldSeperator = ','; // It defines field seperate chracter
    private string[] coinsPose;
    [HideInInspector] public PlayerMovement Player;
    [HideInInspector] public logSys log;

    private void Awake()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        coinsPose = poseListCSV.text.Split(lineSeperater); // load data of locations 
        NumOfCoins = coinsPose.Length;
        Debug.Log("Total coins" + NumOfCoins.ToString("F8"));
        nxtObj();
    }

    public void nxtObj()
    {
        if (NumOfCoins > counter + 1)
        {
            string line = coinsPose[counter];
            string[] moveField = line.Split(fieldSeperator);

            //Debug.Log((moveField.Length).ToString("F8"));

            Vector3 nxtCoinPlace = new Vector3(
                float.Parse(moveField[0]),
                float.Parse(moveField[1]),
                float.Parse(moveField[2]));

            Debug.Log(nxtCoinPlace.ToString("F8"));
            Instantiate(coin, nxtCoinPlace, Quaternion.identity);
            
            Debug.Log((counter % 3).ToString());

            if (counter%3 == 1 && Player.continusMode)
            {
                Player.DisplayQuiz();
                Player.moveIsEnable = false;
            }

            counter += 1;

            int coinsLeft = NumOfCoins - counter;
            string msg = coinsLeft.ToString() + "  Coins left to collect";
            Player.dispExtraMsg(msg);
        }
        else
        {
            log = GameObject.FindWithTag("log").GetComponent<logSys>();
            Player.destExtraMsg();
            Player.disp("stage completed!");
            string msg = "stageCompleted";
            int lvlNum = SceneManager.GetActiveScene().buildIndex;
            log.updateEventLog(lvlNum, msg); // update log file
            NextStage();
        }

       
    }


    public void NextStage()
    {
        Debug.Log("next stage rigth now");
        Invoke("LoadNextLevel", 3.0f);
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(0);
    }

}
