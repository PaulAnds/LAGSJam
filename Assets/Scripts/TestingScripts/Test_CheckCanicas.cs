using System;
using UnityEngine;

public class Test_CheckCanicas : MonoBehaviour
{
    public GameObject objectToSpawn;
    public float scoreValue; 
    public bool ballIn;
    public Test_DestroyProjectiles originSpawnLocation;
    public PlayerMinigameActions playerActionRef;
    public GameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        playerActionRef = FindFirstObjectByType<PlayerMinigameActions>();
        originSpawnLocation = FindFirstObjectByType<Test_DestroyProjectiles>();
    }
    void OnTriggerEnter(Collider other)
    {
        gameManager.totalPointsMarble += scoreValue;
        if(gameManager.totalPointsMarble >= 199f){
            gameManager.hasWonMarble = true;
            playerActionRef.ExitGame();
        }
        else{
        
            if(gameManager.numberOfCurrentTries < gameManager.numberOfTriesMarble - 1f){
                gameManager.numberOfCurrentTries++;
                gameManager.ChangeTextMarbleLeft(Math.Abs(gameManager.numberOfCurrentTries-5));
                print("try "+ gameManager.numberOfCurrentTries);
            }
            else{
                gameManager.numberOfCurrentTries = 0f;
                gameManager.totalPointsMarble = 0f;
                gameManager.ChangeTextMarbleLeft(5f);
                print("Reset");
            }

            ballIn = true;
            Destroy(playerActionRef.marble);
            GameObject newObject = Instantiate(objectToSpawn,originSpawnLocation.spherePosition, new Quaternion(0,0,0,0));
            playerActionRef.marble = newObject;
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
