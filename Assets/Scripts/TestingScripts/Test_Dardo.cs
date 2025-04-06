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
            Destroy(other.gameObject);
            gameManager.numberOfBalloonsToWin--;
            if(gameManager.numberOfBalloonsToWin <= 0){
                playerActions.ExitGame();
                gameManager.hasWonDarts = true;

            }
            transform.position = originalSpawn;
            playerActions.choosingDartPosition = true;
            playerActions.shootingDart = false;
        }
        if(other.gameObject.tag == "Board"){
            ResetBoard();
        }
    }

    public void ResetBoard(){
        //respawn new board to reset
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
