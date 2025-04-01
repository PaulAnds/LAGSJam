using UnityEngine;

public class Test_Interactables : MonoBehaviour, IInteractable
{
    public Transform positionToMove;
    public PlayerMovement playermovement;
    public PlayerCamera playercamera;


    void Start()
    {
        playermovement = FindFirstObjectByType<PlayerMovement>();    
        playercamera = FindFirstObjectByType<PlayerCamera>();    
    }

    public void StartInteraction(){
        print("Interacted");
        playermovement.canMove = false;
        positionToMove = playercamera.newCameraLocation;
        playercamera.moveCamera = true;
    }


    void Update()
    {
        if(playercamera.moveCamera){
            playercamera.MoveCameraTo(positionToMove);
            if(playercamera.playerCamera.transform.position == positionToMove.position){    
                playercamera.moveCamera = false;
            }
        }
    }
}
