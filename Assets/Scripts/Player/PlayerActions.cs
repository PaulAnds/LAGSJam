using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class PlayerStats : MonoBehaviour
{
    [HideInInspector]
    public enum CurrentGame {none, canica, football, dardo }


    [Header("Limit Values")]
    public float timeBeforeDeath = 10f;
    [Header("Current Game")]
    public CurrentGame currentGame = CurrentGame.none;

    //these are used if we want the "cooldown" of the heartbeat to go down or up faster, i imagine these will be used with the enemies?
    private float agitationIncrement = 1f;
    private float agitationDecrement = 1f;
    private float heartRate = 1f;
    private AudioSource heartBeatSound;
    private Volume CameraVolume;
    private Vignette volumeSettings; 
    private PlayerMovement playermovement;
    private PlayerCamera playerCameraRef;

    void Start()
    {
        playerCameraRef = GetComponentInChildren<PlayerCamera>();
        heartBeatSound = GetComponent<AudioSource>();
        playermovement = GetComponent<PlayerMovement>();
        CameraVolume = GetComponentInChildren<Volume>();
    }

    void Update()
    {
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

}
