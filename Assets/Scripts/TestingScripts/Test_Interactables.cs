using UnityEngine;

public class Test_Interactables : MonoBehaviour, IInteractable
{

    public Transform newCameraLocation;
    public PlayerMovement playermovement;
    public PlayerCamera playercamera;
    public bool moveCamera = false;


    void Start()
    {
        newCameraLocation = transform.Find("CameraPlacement");
        playermovement = FindFirstObjectByType<PlayerMovement>();    
        playercamera = FindFirstObjectByType<PlayerCamera>();    
    }

    public void StartInteraction(){
        print("Interacted");
        playermovement.canMove = false;
        moveCamera = true;
    }


    void Update()
    {
        if(moveCamera){
            if(playercamera.playerCamera.transform.position != newCameraLocation.position){
                playercamera.playerCamera.transform.position = Vector3.Lerp(playercamera.playerCamera.transform.position, newCameraLocation.position, 5f * Time.deltaTime);
                playercamera.playerCamera.transform.rotation = Quaternion.Lerp(playercamera.playerCamera.transform.rotation,newCameraLocation.transform.rotation,5f* Time.deltaTime);
            }
            else{
                moveCamera = false;
            }
        }
    }
}
