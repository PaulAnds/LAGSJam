using UnityEngine;
using UnityEngine.Animations.Rigging;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerStats : MonoBehaviour
{
    //[HideInInspector]
    public enum CurrentGame {none, marble, soccer, dart }


    [Header("Limit Values")]
    public float timeBeforeDeath = 10f;
    [Header("Current Game")]
    public CurrentGame currentGame = CurrentGame.none;
    [Header("Parameters")]
    public bool hasEyesClosed = false;

    //these are used if we want the "cooldown" of the heartbeat to go down or up faster, i imagine these will be used with the enemies?
    private float agitationIncrement = 1f;
    private float agitationDecrement = 1f;
    public float heartRate = 1f;
    private AudioSource heartBeatSound;
    private Volume CameraVolume;
    private Vignette volumeSettings; 
    private ColorAdjustments colorSettings; 
    private PlayerMovement playermovement;
    private PlayerCamera playerCameraRef;
    private MultiAimConstraint headRotation;
    private MultiAimConstraint bodyRotation;

    void Start()
    {
        playerCameraRef = GetComponentInChildren<PlayerCamera>();
        heartBeatSound = GetComponent<AudioSource>();
        playermovement = GetComponent<PlayerMovement>();
        CameraVolume = GetComponentInChildren<Volume>();
        headRotation = GetComponentInChildren<MultiAimConstraint>();
        //xd
        bodyRotation = gameObject.transform.GetChild(2).GetChild(5).GetChild(1).GetComponent<MultiAimConstraint>();
    }

    void Update()
    {

        if(heartRate >= 9.5){
            print("Dead HeartAttack");
        }
        if(playermovement.canMove)
        {
            if (Input.GetKey(KeyCode.Q)){
                //print("eyes closed....Heart Rate: " + heartRate);
                //audio speed
                heartRate = heartRate + (Time.deltaTime * agitationIncrement);
                heartBeatSound.pitch = RemapRange(heartRate,0f,timeBeforeDeath,.5f,3f);

                //Add Darkness
                if (CameraVolume.profile.TryGet(out volumeSettings))
                {
                    if(volumeSettings.intensity.value<=1){
                        volumeSettings.intensity.value += Time.deltaTime;
                    }
                    if(volumeSettings.intensity.value>=0.9f){
                        hasEyesClosed = true;
                    }
                }
                if(CameraVolume.profile.TryGet(out colorSettings)){
                    if(colorSettings.postExposure.value >= -8){
                        colorSettings.postExposure.value -= Time.deltaTime*8f;
                    }
                }
            }
            else{
                if(heartRate > 0){
                    //print("eyes open....Heart Rate: " + heartRate);
                    //audio speed
                    heartRate = heartRate - (Time.deltaTime * agitationDecrement);
                    heartBeatSound.pitch = RemapRange(heartRate,0f,timeBeforeDeath,.5f,3f);

                    //Remove Darkness
                    if (CameraVolume.profile.TryGet(out volumeSettings))
                    {
                        if(volumeSettings.intensity.value>=0){
                            volumeSettings.intensity.value -= Time.deltaTime;
                        }
                        if(volumeSettings.intensity.value<=0.9f){
                            hasEyesClosed = false;
                        }
                    }
                    if(CameraVolume.profile.TryGet(out colorSettings)){
                        if(colorSettings.postExposure.value <= 0){
                            colorSettings.postExposure.value += Time.deltaTime*8f;
                    }
                }
                }
                else{
                    heartBeatSound.pitch = 0f;
                }
            }

            //Raycast for interaction
            if (Input.GetKeyDown(KeyCode.E) && !playerCameraRef.moveCamera){
                RaycastHit hit;
                if (Physics.Raycast(playerCameraRef.playerCamera.transform.position, playerCameraRef.playerCamera.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
                { 
                    //Debug.Log("Did Hit"); 
                    Debug.DrawRay(playerCameraRef.playerCamera.transform.position, playerCameraRef.playerCamera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);

                    //This checks to see if the object hit by racast has the interface IInteractable
                    IInteractable Iinteractable = hit.collider.gameObject.GetComponent<IInteractable>();
                    if (Iinteractable != null)
                    {
                        Iinteractable.StartInteraction(hit.collider.gameObject);
                    }
                }
                else{
                Debug.DrawRay(playerCameraRef.playerCamera.transform.position, playerCameraRef.playerCamera.transform.TransformDirection(Vector3.forward) * 1000, Color.yellow);
                }
            }
        }
    }

    //This lets me make remap a value, using this formula from Godot cite (https://victorkarp.com/godot-engine-how-to-remap-a-range-of-numbers/) 
    //Used it to make the heartbeat variable move towards the pitch which is .5 - 3 which is our output variables and the input variables are 0 - (time player can keep eyes closed).
    private float RemapRange(float value, float InputA, float InputB, float OutputA, float OutputB)
    {
        float newvalue = (value - InputA) / (InputB - InputA) * (OutputB - OutputA) + OutputA;

        if(newvalue >= 3f){
            return 3f;
        }
        else{
            return (value - InputA) / (InputB - InputA) * (OutputB - OutputA) + OutputA;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        int index = -1;
        
        switch(other.gameObject.tag){
            case "MarbleGame":
                    index = 0;
                break;     
            case "DartGame":
                    index = 1;
                break;               
            case "SoccerGame":
                    index = 2;
                break;
            default:
                index = -1;
                break;
        }
        var test = headRotation.data.sourceObjects;
        //set this to use lerp
        test.SetWeight(index,1f);
        headRotation.data.sourceObjects = test;
        bodyRotation.data.sourceObjects = test;
    }

    void OnTriggerExit(Collider other)
    {
        int index = -1;
        
        switch(other.gameObject.tag){
            case "MarbleGame":
                    index = 0;
                break;     
            case "DartGame":
                    index = 1;
                break;               
            case "SoccerGame":
                    index = 2;
                break;
            default:
                index = -1;
                break;
        }
        var test = headRotation.data.sourceObjects;
        //set this to use lerp

        test.SetWeight(index,0f);
        headRotation.data.sourceObjects = test;
        bodyRotation.data.sourceObjects = test;
    }

}
