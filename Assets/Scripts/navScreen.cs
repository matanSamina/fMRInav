using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class navScreen : MonoBehaviour
{
    // Start is called before the first frame update

    public string imagePath = "pics/FEG1city";
    private Texture2D[] typedTextures;
    public int target;
    private Object[] textures;

    public PlayerMovement Player;

    private bool startNav = false;
    private int trueVal;
    public logSys log;

    public float xTarget;
    public float zTarget;

    public Vector2 a;
    public Vector2 b;
    public float dist;

    public bool showImage = false;

    private float timeRemaining = 3.0f;
    private bool nxt = true;

    void Start()
    {
        showImage = true;
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        target = 0;
        Player.disp("go to target" , 2.0f);
        loadImage();
    }


    void OnGUI()
    {
        if (typedTextures != null && showImage)
        {
            float width = typedTextures[target].width;
            float height = typedTextures[target].height;
            GUI.DrawTexture(new Rect( 0 , 0, Screen.width / 3, Screen.height / 3), typedTextures[target], ScaleMode.StretchToFill, true);
        }
    }

    public void loadImage()
    {
        textures = Resources.LoadAll(imagePath, typeof(Texture2D));
        typedTextures = new Texture2D[textures.Length];
        for (int i = 0; i < textures.Length; i++) { typedTextures[i] = (Texture2D)textures[i]; }
        Debug.Log("Textures Loaded: " + typedTextures.Length);
        //Debug.Log("Image Path: " + imagePath);
        string poseOfimage = typedTextures[target].name;
        //Debug.Log("Image Name: " + typedTextures[target].name);
        string[] cordinates = poseOfimage.Split(',');
        Debug.Log("Target position: " +"x = "+cordinates[1] +",z = " + cordinates[3] );  
        zTarget = float.Parse(cordinates[3]);
        xTarget = float.Parse(cordinates[1]);
        b = new Vector2(xTarget, zTarget); 
        //Debug.Log("Target position: " +"x = "+ xTarget.ToString + ",z = " + zTarget.ToString );

        log = GameObject.FindWithTag("log").GetComponent<logSys>();
        Invoke("sNav", 1.0f);
        
    }

    void sNav()
    {
        startNav = true;
    }

    void Update()
    {
        if (startNav)
        {      
            a = Player.playerCpose;
            dist = (a - b).sqrMagnitude;
            //Debug.Log(dist.ToString);
            if(dist < 6)
            {
                timeRemaining -= Time.deltaTime;
                if(timeRemaining < 0 && nxt){ 
                    Player.disp("Great! you reached goal", 2.0f);
                    Invoke("nxtTarget", 2.0f);
                    nxt = false;
                } 
            }
            else { timeRemaining = 3.0f; }
        }
    }

    private void nxtTarget()
    {

        showImage = false;
        if(target < typedTextures.Length -1)
        {
            target += 1;
            Invoke("showIm", 2.0f);
            Player.disp("go to Next location", 4.0f);
        }
        else
        {
           
            Player.disp("Task completed!", 4.0f);
            Invoke("LoadNextLevel", 3.0f);
        }
        

        string poseOfimage = typedTextures[target].name;
        //Debug.Log("Image Name: " + typedTextures[target].name);

        string[] cordinates = poseOfimage.Split(',');
        Debug.Log("Target position: " + "x = " + cordinates[1] + ",z = " + cordinates[3]);
        zTarget = float.Parse(cordinates[3]);
        xTarget = float.Parse(cordinates[1]);
        b = new Vector2(xTarget, zTarget);

    }

    private void showIm()
    {
        showImage = true;
        nxt = true;
    }


    private void LoadNextLevel()
    {
        SceneManager.LoadScene(0);
    }
}
