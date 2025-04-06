using UnityEditor;
using UnityEngine;

public class Test_CheckGoal : MonoBehaviour
{
    public bool isTrue;
    public GameManager gameManager;
    public PlayerMinigameActions playerActions;

    void Start()
    {
        playerActions = FindAnyObjectByType<PlayerMinigameActions>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Projectile"){
            gameManager.goalNumber++;
            if(isTrue){
                gameManager.soccerGoals[gameManager.goalNumber-1] = 1;
                print("true");
            }
            else{
                gameManager.soccerGoals[gameManager.goalNumber-1] = 0;
                print("false");
            }
            if(gameManager.soccerGoals[0] == 1 && gameManager.soccerGoals[1] == 0 && gameManager.soccerGoals[2] == 1 && gameManager.soccerGoals[3] == 0 && gameManager.soccerGoals[4] == 1){
                print("win soccer");
                gameManager.hasWonSoccer = true;
                playerActions.ExitGame();
                gameManager.playerAS.clip = gameManager.victoryMinigame;
                gameManager.playerAS.Play();
            }
            if(gameManager.goalNumber >= 5){
                gameManager.goalNumber = 0;
                gameManager.playerAS.clip = gameManager.loseMinigame;
                gameManager.playerAS.Play();
                for(int i = 0; i < 5; i++){
                    gameManager.soccerGoals[i] = -1;
                }
            }
            

        }
    }
}
