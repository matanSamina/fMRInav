using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class quizScreen : MonoBehaviour
{
    // Start is called before the first frame update

    public string imagePath = "pics/mountains";
    private Texture2D[] typedTextures;
    private int PicNum = 2;
    private Object[] textures;
    private float quizTime = 10f;
    public PlayerMovement Player;
    private float ans = 0.0f;
    public Slider slider;
    private char isTrue;
    private bool startQuiz = false;
    private int trueVal;
    public logSys log;
    private float tDest = 1.0f;
    private int playerAnsVal;

    void Start()
    {
        Player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        Player.rend(true);
        Player.destExtraMsg();
        loadImages();
    }

    private void Dest()
    {
        if (trueVal == playerAnsVal)
        {
            Player.disp("correct answer!" , 2.0f);
        }
        else
        {
            Player.disp("wrong answer" , 2.0f);
        }

        Destroy(gameObject);
    }

    void OnGUI()
    {
        if (typedTextures != null && typedTextures.Length > 0)
        {
            float width = typedTextures[PicNum].width;
            float height = typedTextures[PicNum].height;
            GUI.DrawTexture(new Rect(Screen.width / 4, Screen.height / 4, Screen.width / 2, Screen.height / 2), typedTextures[PicNum], ScaleMode.StretchToFill, true);
        }
    }

    public void loadImages()
    {
        textures = Resources.LoadAll(imagePath, typeof(Texture2D));
        typedTextures = new Texture2D[textures.Length];
        for (int i = 0; i < textures.Length; i++){typedTextures[i] = (Texture2D)textures[i];}
        PicNum = Random.Range(1, textures.Length);

        Debug.Log("Textures Loaded: " + typedTextures.Length);
        Debug.Log("Image Path: " + imagePath);
        Debug.Log("Image Name: " + typedTextures[PicNum].name);
        isTrue = typedTextures[PicNum].name[0];

        if (isTrue == 't'){trueVal = 1;}
        else{trueVal = 0;}


        log = GameObject.FindWithTag("log").GetComponent<logSys>();
        quizTime = Player.quizTime;
        Invoke("Dest", quizTime);
        Invoke("sQuiz", 1.0f);
    }

    void sQuiz()
    {
        startQuiz = true;
    }

    void Update()
    {
        if (startQuiz)
        {
            ans = Input.GetAxis("Horizontal");
            slider.value += ans/4;
            if(slider.value < 0.1)
            {
                string msg = "Q,1";
                playerAnsVal = 1;
                Debug.Log("AnsTrue");
                log.updateEventLog(trueVal, msg); // update log file
                Invoke("Dest", tDest);
            }
            else if (slider.value > 0.9)
            {
                string msg = "Q,0";
                playerAnsVal = 0;
                Debug.Log("AnsFalse");
                log.updateEventLog(trueVal, msg); // update log file
                Invoke("Dest", tDest);
            }
        }

    }

}
