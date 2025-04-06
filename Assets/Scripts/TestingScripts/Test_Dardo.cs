using UnityEngine;

public class Test_Dardo : MonoBehaviour
{
    private Vector3 originalSpawn;
    private PlayerMinigameActions playerActions;
    private GameManager gameManager;
    private GameObject oldGameBoardRef;
    public GameObject newGameBoard;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        playerActions = FindFirstObjectByType<PlayerMinigameActions>();
        originalSpawn = transform.position;
        oldGameBoardRef = GameObject.Find("BoardBalloon");
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Balloon"){
            switch(other.gameObject.GetComponent<Test_ColorCheck>().currentColor.ToString()){
                case "purple" :
                    gameManager.balloonsToHit[0]++;
                    break;
                case "blue" :
                    gameManager.balloonsToHit[1]++;
                    break;
                case "green" :
                    gameManager.balloonsToHit[2]++;
                    break;
                case "yellow" :
                    gameManager.balloonsToHit[3]++;
                    break;
                case "red" :
                    gameManager.balloonsToHit[4]++;
                    break;
            }
            if(gameManager.balloonsToHit[0] == 2 && gameManager.balloonsToHit[1] == 1 && gameManager.balloonsToHit[2] == 3 && gameManager.balloonsToHit[3] == 2 && gameManager.balloonsToHit[4] == 2 )
            {
                print("won");
                gameManager.hasWonDarts = true;
            }
            Destroy(other.gameObject);
            gameManager.numberOfBalloonsToWin--;
            gameManager.playerAS.clip = gameManager.balloonPop;
            gameManager.playerAS.Play();
            if(gameManager.numberOfBalloonsToWin <= 0){
                playerActions.ExitGame();
                gameManager.hasWonDarts = true;
                gameManager.playerAS.clip = gameManager.victoryMinigame;
                gameManager.playerAS.Play();

            }
            transform.position = originalSpawn;
            playerActions.choosingDartPosition = true;
            playerActions.shootingDart = false;
        }
        if(other.gameObject.tag == "Board"){
            ResetBoard();
            gameManager.playerAS.clip = gameManager.loseMinigame;
            gameManager.playerAS.Play();
        }
    }

    public void ResetBoard(){
        //respawn new board to reset
        for(int i = 0; i < 5; i++){
            gameManager.balloonsToHit[i] = 0;
        }
        Vector3 position = oldGameBoardRef.transform.position;
        Quaternion rotation = oldGameBoardRef.transform.rotation;
        Destroy(oldGameBoardRef);
        oldGameBoardRef = Instantiate(newGameBoard,position,rotation);
        //reset parameters
        playerActions.choosingDartPosition = true;
        playerActions.shootingDart = false;
        //reset win condition
        gameManager.numberOfBalloonsToWin = 10f;
    }
}
