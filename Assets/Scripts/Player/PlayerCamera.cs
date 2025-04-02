using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    
    [Header("Camera")]
    public float lookSpeed = 180f;
    public float lookDOWNLimit = 45f;
    public float lookUPLimit = -14f;

    private float rotationX = 0;
    private PlayerMovement playermovement;

    //debug float, since lerp takes forever to finish
    private float floatToStopWaitingTime = 0f;

    //These have references in other scripts, need them public 
    [HideInInspector]
    public Camera playerCamera;
    [HideInInspector]
    public bool moveCamera = false;
    [HideInInspector]
    public Transform oldCameraLocation;
    [HideInInspector]
    public Transform positionToMove;
    [HideInInspector]
    public GameObject newCameraLocation;

    void Start()
    {
        playerCamera = GetComponentInChildren<Camera>();
        playermovement = GetComponent<PlayerMovement>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        oldCameraLocation = transform.Find("PlayerCameraPosition");
    }

    void Update()
    {
        if(playermovement.canMove){   
            //Camera Movement
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed * Time.deltaTime;
            rotationX = Mathf.Clamp(rotationX, lookUPLimit, lookDOWNLimit);
            playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
            transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed * Time.deltaTime, 0);
        }
    }

    public void MoveCameraTo(Transform positionToMove){
        if(playerCamera.transform.position != positionToMove.position){
            playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, positionToMove.position, 4f * Time.deltaTime);
            playerCamera.transform.rotation = Quaternion.Lerp(playerCamera.transform.rotation,positionToMove.transform.rotation, 4f * Time.deltaTime);
            floatToStopWaitingTime += Time.deltaTime;
            if(floatToStopWaitingTime >= 2f){
                playerCamera.transform.position = positionToMove.position;
                floatToStopWaitingTime = 0f;
            }
        }
    }
}
