using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class timebar : MonoBehaviour
{
    //public Slider slider;
    public TextMeshProUGUI txt;
    public TextMeshProUGUI txtOfCoins;
    public float timeForGame;
    private float t = 0;
    [SerializeField] private GameObject coinsSys;
    private coinsS coins;
    private bool onPlay = false;
    public PlayerMovement player;
    bool teleportationMode;
    bool continusMode;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        teleportationMode = player.teleportationMode;
        continusMode = player.continusMode;

        if (teleportationMode == true || continusMode == true)
        {
            coinsSys = GameObject.FindWithTag("masterCoin"); // perent of tube 1 segments
            coins = coinsSys.GetComponent<coinsS>();
        }


        //slider.value = 0f;
        
    }

    // Update is called once per frame
    void Update()
    {
        
        onPlay = player.moveIsEnable;
        if (player.Tpressed && onPlay) {

            t += Time.deltaTime;
            //slider.value = t / timeForGame;

            if (teleportationMode == true || continusMode == true)
            {
                float timeRe = timeForGame - t;
               // txt.text = "Time Remaining " + timeRe.ToString("N2") + "sec";
            }
            else
            {   
                float timeRe = player.timeForFreeExploratopn - t;
                if (timeRe > 0.0f){
                 //   txt.text = "Time remaining  " + timeRe.ToString("N2") + " sec";
                  //  txtOfCoins.text =  " ";
                }
                else
                {
               //     txt.text = "Find Locations ";
                 //   txtOfCoins.text = " Targets Left";
                }
            }
        }

    }
}
