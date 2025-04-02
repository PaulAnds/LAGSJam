using UnityEngine;

public class Test_Interactables : MonoBehaviour, IInteractable
{
    public PlayerMovement playermovement;
    public PlayerCamera playercamera;

    void Start()
    {
        playermovement = FindFirstObjectByType<PlayerMovement>();    
        playercamera = FindFirstObjectByType<PlayerCamera>();    
    }

    public void StartInteraction(){
        //print("Interacted");
        playermovement.canMove = false;
        playercamera.positionToMove = playercamera.newCameraLocation;
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
