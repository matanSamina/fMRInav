#if ENABLE_INPUT_SYSTEM 
using UnityEngine.InputSystem;
#endif

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.IO;
using TMPro;

public class PlayerMovement : MonoBehaviour
{

    // GAMES MODES
    
    public bool teleportationMode = false;
    public bool fMRImode = false;
    public bool freeExplorationMode = true;
    public bool continusMode = false;

    private bool gameStart = false;
    
    public CharacterController controller;
    private float speed = 6f;
       
    public GameObject blackScreenPrefab;
    private Vector3 velocity;

    [HideInInspector] public int nextPlace;
    public TextAsset teleportCSV; // Reference of CSV file
    private char lineSeperater = '\n'; // It defines line seperate character
    private char fieldSeperator = ','; // It defines field seperate chracter
    [HideInInspector] public bool eatCoin = false;
    private string[] playerPose;
    private int currentTime;
    
    
    [HideInInspector] public bool moveIsEnable = false;
    private string[] moveField;
    public GameObject LogObj;

    public GameObject QueezblackScreenPrefab;
    public string picsFolderForQuiz = "pics/mountains";
    public float quizTime = 10f;

    public GameObject bs;
    private float z;

    [HideInInspector] public bool Tpressed = false;

    // free exploration variables 
    [HideInInspector] public int step = 0;  // step of player 
    public string picsFolderForNav = "pics/FEG1city";
    public GameObject navScreenPrefab;
    [HideInInspector] public Vector2 playerCpose;
    public float timeForFreeExploratopn;
    float t = 0.0f;

    // main GUI msg
    [HideInInspector] public GameObject dispText;
    [HideInInspector] private string GuiMsg;

    // side GUI msg
    [HideInInspector] public GameObject dispExtraText;
    [HideInInspector] private string GuiExtraMsg;


    // player movment variables
    [HideInInspector] bool isGrounded;
    [HideInInspector] public float gravity = -10f;
    [HideInInspector] public float jumpHeight = 2f;

    public Transform groundCheck;
    [HideInInspector] public float groundDistance = 0.4f;
    public LayerMask groundMask;

    void Awake()
    {
        dispText = GameObject.FindWithTag("notes");
        dispExtraText = GameObject.FindWithTag("extra");
        disp("press T to start" , 0.0f);
    }

    void gameBegin()
    {
        Instantiate(LogObj); // start log file 
        
        if (fMRImode)
        {
            float timeto = 4.0f;
            string GuiMsg = "Stage start in " + timeto.ToString() + " second";
            Invoke("enableMovmentStart", timeto);
        }

        if (teleportationMode)
        {
            currentTime = (int)Time.time; // time for the score
            playerPose = teleportCSV.text.Split(lineSeperater); // load data of locations 
            moveTo(0);
            enableMovmentStart();
        }
        //Application.targetFrameRate = 20;   
        if (freeExplorationMode)
        {

            Invoke("DisplayTarget", timeForFreeExploratopn);
            disp("explore enviroment for " + timeForFreeExploratopn.ToString() + " seconed", 4.0f);
            enableMovmentStart();
        }

        if (continusMode)
        {
            enableMovmentStart();
        }

        gameStart = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Tpressed = true;
            gameBegin();
            WriteToLog("Trigger");
            Debug.Log("Tpressed");
            destMsg();
        }

        if (moveIsEnable)
        {
            z = Input.GetAxis("Vertical");
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            Vector3 move = transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            velocity.y += gravity * Time.deltaTime;

            controller.Move(velocity * Time.deltaTime);




            //z = Input.GetAxis("Vertical");
            //isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            //if (isGrounded && velocity.y < 0)
           // {
                velocity.y = -2f;
            //}

         //   Vector3 move = transform.forward * z;
          //  controller.Move(move * speed * Time.deltaTime);
          //  velocity.y += gravity * Time.deltaTime;
         //   controller.Move(velocity * Time.deltaTime);
        }

        if (freeExplorationMode)
        {
            if (gameStart)
            {
                t += Time.deltaTime;
                float timeRe = timeForFreeExploratopn - t;
                if (timeRe >= 0) {
          
                    string tempMsg = "Exploration mission start in " + timeRe.ToString("N2") + "sec";
                    dispExtraMsg(tempMsg, 0);
                }
                else
                {
                    destExtraMsg();
                }
            }
            // capture player position for navScreen script 
            playerCpose = new Vector2(transform.position.x, transform.position.z);
        }
       
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space key was pressed");
            SceneManager.LoadScene(0);
        }

    }

    private void WriteToLog(string msg)
    {
        // call log file and write msg to log file 
        logSys log = GameObject.FindWithTag("log").GetComponent<logSys>();
        log.updateEventLog(0, msg); // update log file
        
    }

    public void rend(bool diaplay = true)
    {
        // enable blackscreen rendring
        bs.GetComponent<MeshRenderer>().enabled = diaplay;
    }

    public void moveTo(int place)
    {
        string line = playerPose[place];
        moveField = line.Split(fieldSeperator);
        moveIsEnable = false;
        rend(false);

        if(place != 0)
        {
            GameObject blackScreen = GameObject.Instantiate(blackScreenPrefab, Vector3.zero , Quaternion.identity);
            blackScreen.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false );
            rend(true);
        }

        Invoke("moveNow", 2.0f);
    }

    private void enableMovmentStart()
    {
        moveIsEnable = true;
        rend(false);
    }

    private void moveNow()
    {
        Vector3 moveBase = new Vector3(
            float.Parse(moveField[0]),
            float.Parse(moveField[1]),
            float.Parse(moveField[2]));

        transform.position = moveBase;
        transform.rotation = Quaternion.Euler(0, float.Parse(moveField[3]), 0);

        moveIsEnable = true;
        rend(false);
    }

    public void disp(string msgToDisp , float timeToDisp = 0.0f)
    {
        dispText.GetComponent<TMPro.TextMeshProUGUI>().text  = msgToDisp;
        moveIsEnable = false;
        if (timeToDisp > 0.0f) { Invoke("destMsg", timeToDisp); }
        rend();
    }
    
    public void dispExtraMsg(string msgToDisp , float timeToDisp = 0.0f)
    {
        dispExtraText.GetComponent<TMPro.TextMeshProUGUI>().text  = msgToDisp;
        if (timeToDisp > 0.0f) { Invoke("destExtraMsg", timeToDisp); }
    }

    private void destMsg()
    {
        GuiMsg = "";
        dispText.GetComponent<TMPro.TextMeshProUGUI>().text  = GuiMsg;
        moveIsEnable = true;
        rend(false);
    }

    public void destExtraMsg()
    {
        dispExtraText.GetComponent<TMPro.TextMeshProUGUI>().text = ""; 
    }

    public void DisplayQuiz()
    {
        GameObject blackScreen = GameObject.Instantiate(QueezblackScreenPrefab, Vector3.zero, Quaternion.identity);
        blackScreen.GetComponent<quizScreen>().imagePath = picsFolderForQuiz;
        blackScreen.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        moveIsEnable = false;
        Debug.Log("quiz is desplaying");
    }

    public void DisplayTarget()
    {

        GameObject navScreen = GameObject.Instantiate(navScreenPrefab, Vector3.zero, Quaternion.identity);
        navScreen.GetComponent<navScreen>().imagePath = picsFolderForNav;
        navScreen.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
        Debug.Log("Nav quiz is desplaying");

    }

    private void enableMovment()
    {
        moveIsEnable = true;
        rend(false);
    }

}
