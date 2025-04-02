using UnityEngine;

public class Test_Interactables : MonoBehaviour, IInteractable
{
    private PlayerMovement playermovement;
    private PlayerCamera playercamera;
    private PlayerStats playerActions;

    void Start()
    {
        playermovement = FindFirstObjectByType<PlayerMovement>();    
        playercamera = FindFirstObjectByType<PlayerCamera>();   
        playerActions = FindFirstObjectByType<PlayerStats>(); 
    }

    public void StartInteraction(GameObject hit){
        //print("Interacted");
        playermovement.canMove = false;
        if(hit.tag == "Canica"){
            playerActions.currentGame = PlayerStats.CurrentGame.canica;
            playercamera.newCameraLocation = GameObject.Find("CameraPlacement_Canica");
        }
        if(hit.tag == "Dardo"){
            playerActions.currentGame = PlayerStats.CurrentGame.dardo;
            playercamera.newCameraLocation = GameObject.Find("CameraPlacement_Dardo");
        }
        if(hit.tag == "Football"){
            playerActions.currentGame = PlayerStats.CurrentGame.football;
            playercamera.newCameraLocation = GameObject.Find("CameraPlacement_Football");
        }
        playercamera.positionToMove = playercamera.newCameraLocation.transform;
        playercamera.moveCamera = true;
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
