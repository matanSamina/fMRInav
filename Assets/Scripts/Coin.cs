using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    //public GameObject coinSys;
    private Vector3 _pos;
    public GameObject CoinsSys;
    private coinsS Score;
    [SerializeField] private GameObject player;
    [SerializeField] private PlayerMovement playerC;
    public logSys log;


    void Awake()
    {
        player = GameObject.FindWithTag("Player"); // perent of tube 1 segments
        playerC = player.GetComponent<PlayerMovement>();

       
        if (playerC.teleportationMode == true)
        {
            Debug.Log("teleport");
        }

        _pos = transform.position;
        CoinsSys = GameObject.FindWithTag("masterCoin");
        Score = CoinsSys.GetComponent<coinsS>();
    }

    private void Update()
    {
        transform.Rotate(new Vector3(0f, 100f, 0f) * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("coin collected");

        log = GameObject.FindWithTag("log").GetComponent<logSys>();
        log.updateEventLog(Score.counter , "coin"); // update log file


        if (playerC.teleportationMode && Score.counter < Score.NumOfCoins-1)
        {
            playerC.moveTo(Score.counter);
        }

        Score.score += 9;
        Score.nxtObj();
        Destroy(gameObject); // destroy coin object
    }


}
