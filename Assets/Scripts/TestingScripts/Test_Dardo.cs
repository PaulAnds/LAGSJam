using UnityEngine;

public class Test_Dardo : MonoBehaviour
{
    private Vector3 originalSpawn;
    private PlayerMinigameActions playerActions;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        playerActions = FindFirstObjectByType<PlayerMinigameActions>();
        originalSpawn = transform.position;
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
    }
}
