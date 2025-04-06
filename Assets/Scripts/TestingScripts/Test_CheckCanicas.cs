using System;
using UnityEngine;

public class Test_CheckCanicas : MonoBehaviour
{
    public GameObject objectToSpawn;
    public GameObject parentOfMarble;
    public float scoreValue; 
    private bool ballIn;
    private Test_DestroyProjectiles originSpawnLocation;
    private PlayerMinigameActions playerActionRef;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        playerActionRef = FindFirstObjectByType<PlayerMinigameActions>();
        originSpawnLocation = FindFirstObjectByType<Test_DestroyProjectiles>();
    }
    void OnTriggerEnter(Collider other)
    {
        gameManager.totalPointsMarble += scoreValue;
        gameManager.playerAS.Stop();
        if(gameManager.totalPointsMarble == 1960f){
            gameManager.hasWonMarble = true;
            playerActionRef.ExitGame();
            gameManager.playerAS.clip = gameManager.victoryMinigame;
            gameManager.playerAS.Play();
        }
        else{
        
            if(gameManager.numberOfCurrentTries < gameManager.numberOfTriesMarble - 1f){
                gameManager.numberOfCurrentTries++;
                gameManager.ChangeTextMarbleLeft(Math.Abs(gameManager.numberOfCurrentTries-5));
            }
            else{
                gameManager.playerAS.clip = gameManager.loseMinigame;
                gameManager.playerAS.Play();
                gameManager.numberOfCurrentTries = 0f;
                gameManager.totalPointsMarble = 0f;
                gameManager.ChangeTextMarbleLeft(5f);
            }

            ballIn = true;
            Destroy(playerActionRef.marble);
            GameObject newObject = Instantiate(objectToSpawn,originSpawnLocation.spherePosition, new Quaternion(0,0,0,0));
            newObject.transform.SetParent(parentOfMarble.transform);
            playerActionRef.marble = newObject;
            playerActionRef.scale = playerActionRef.marble.transform.GetChild(0).GetChild(0).gameObject;
            originSpawnLocation.marble = newObject;
            playerActionRef.rb = playerActionRef.marble.GetComponent<Rigidbody>(); 
            originSpawnLocation.rb = playerActionRef.marble.GetComponent<Rigidbody>(); 
            playerActionRef.charging = false;
        }
    }
    void OnTriggerExit(Collider other)
    {
        ballIn = false;
    }
}
