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
        if(hit.tag == "Canica" && !gameManager.hasWonCanicas){
            playerActions.currentGame = PlayerStats.CurrentGame.canica;
            playercamera.newCameraLocation = GameObject.Find("CameraPlacement_Canica");
            playermovement.canMove = false;
            playercamera.positionToMove = playercamera.newCameraLocation.transform;
            playercamera.moveCamera = true;
        }
        if(hit.tag == "Dardo" && !gameManager.hasWonDardos){
            playerActions.currentGame = PlayerStats.CurrentGame.dardo;
            playercamera.newCameraLocation = GameObject.Find("CameraPlacement_Dardo");
            playermovement.canMove = false;
            playercamera.positionToMove = playercamera.newCameraLocation.transform;
            playercamera.moveCamera = true;
        }
        if(hit.tag == "Football" && !gameManager.hasWonFootball){
            playerActions.currentGame = PlayerStats.CurrentGame.football;
            playercamera.newCameraLocation = GameObject.Find("CameraPlacement_Football");
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
