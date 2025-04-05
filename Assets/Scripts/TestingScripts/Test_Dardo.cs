using UnityEngine;

public class Test_Dardo : MonoBehaviour
{
    public GameObject dart;
    public Vector3 originalSpawn;
    public PlayerMinigameActions playerActions;
    public GameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        playerActions = FindFirstObjectByType<PlayerMinigameActions>();
        dart = GameObject.Find("Dart");
        originalSpawn = dart.transform.position;
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
            dart.transform.position = originalSpawn;
            playerActions.choosingDartPosition = true;
            playerActions.shootingDart = false;
        }
    }
}
