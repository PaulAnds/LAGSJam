using UnityEngine;

public class Test_Dardo : MonoBehaviour
{
    public GameObject dardo;
    public Vector3 originalSpawn;
    public PlayerMinigameActions playerActions;
    public GameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        playerActions = FindFirstObjectByType<PlayerMinigameActions>();
        dardo = GameObject.Find("Dardo");
        originalSpawn = dardo.transform.position;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Balloon"){
            Destroy(other.gameObject);
            gameManager.numberOfBalloonsToWin--;
            if(gameManager.numberOfBalloonsToWin <= 0){
                playerActions.ExitGame();
                gameManager.hasWonDardos = true;

            }
            dardo.transform.position = originalSpawn;
            playerActions.choosingDartPosition = true;
            playerActions.shootingDart = false;
        }
    }
}
