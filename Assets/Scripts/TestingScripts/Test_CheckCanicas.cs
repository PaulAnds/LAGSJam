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
    private float numberOfCurrentTries;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        playerActionRef = FindFirstObjectByType<PlayerMinigameActions>();
        originSpawnLocation = FindFirstObjectByType<Test_DestroyProjectiles>();
    }
    void OnTriggerEnter(Collider other)
    {
        gameManager.totalPointsCanicas += scoreValue;
        if(gameManager.totalPointsCanicas >= 199f){
            gameManager.hasWonCanicas = true;
            playerActionRef.ExitGame();
        }
        else{
        
            if(numberOfCurrentTries < gameManager.numberOfTriesCanicas - 1f){
                numberOfCurrentTries++;
                gameManager.ChangeTextCanicasLeft(Math.Abs(numberOfCurrentTries-5));
            }
            else{
                numberOfCurrentTries = 0f;
                gameManager.totalPointsCanicas = 0f;
                gameManager.ChangeTextCanicasLeft(5f);
            }

            ballIn = true;
            Destroy(playerActionRef.canica);
            GameObject newObject = Instantiate(objectToSpawn,originSpawnLocation.spherePosition, new Quaternion(0,0,0,0));
            playerActionRef.canica = newObject;
            originSpawnLocation.canica = newObject;
            playerActionRef.rb = playerActionRef.canica.GetComponent<Rigidbody>(); 
            originSpawnLocation.rb = playerActionRef.canica.GetComponent<Rigidbody>(); 
            playerActionRef.charging = false;
        }
    }
    void OnTriggerExit(Collider other)
    {
        ballIn = false;
    }
}
