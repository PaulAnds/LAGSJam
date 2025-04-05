using UnityEngine;

public class Test_Interactables : MonoBehaviour, IInteractable
{
    private PlayerMovement playermovement;
    private PlayerCamera playercamera;
    private PlayerStats playerActions;
    private GameManager gameManager;

    void Start()
    {
        gameManager = FindFirstObjectByType<GameManager>();
        playermovement = FindFirstObjectByType<PlayerMovement>();    
        playercamera = FindFirstObjectByType<PlayerCamera>();   
        playerActions = FindFirstObjectByType<PlayerStats>(); 
    }

    public void StartInteraction(GameObject hit){
        //print("Interacted");
        //las repeti en todas porque el chiste es que no quiero que nada pase si tiene alguno de los booleandos
        if(hit.tag == "MarbleGame" && !gameManager.hasWonMarble){
            playerActions.currentGame = PlayerStats.CurrentGame.marble;
            playercamera.newCameraLocation = GameObject.Find("CameraPlacement_Marble");
            playermovement.canMove = false;
            playercamera.positionToMove = playercamera.newCameraLocation.transform;
            playercamera.moveCamera = true;
        }
        if(hit.tag == "DartGame" && !gameManager.hasWonDarts){
            playerActions.currentGame = PlayerStats.CurrentGame.dart;
            playercamera.newCameraLocation = GameObject.Find("CameraPlacement_Dart");
            playermovement.canMove = false;
            playercamera.positionToMove = playercamera.newCameraLocation.transform;
            playercamera.moveCamera = true;
        }
        if(hit.tag == "SoccerGame" && !gameManager.hasWonSoccer){
            playerActions.currentGame = PlayerStats.CurrentGame.soccer;
            playercamera.newCameraLocation = GameObject.Find("CameraPlacement_Soccer");
            playermovement.canMove = false;
            playercamera.positionToMove = playercamera.newCameraLocation.transform;
            playercamera.moveCamera = true;
        }
    }


    void Update()
    {
        if(playercamera.moveCamera){
            playercamera.MoveCameraTo(playercamera.positionToMove);
            if(playercamera.playerCamera.transform.position == playercamera.positionToMove.position){    
                playercamera.moveCamera = false;
            }
        }
    }
}
