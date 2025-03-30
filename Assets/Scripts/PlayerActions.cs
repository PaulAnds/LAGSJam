using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private float heartRate = 0f;
    public float timeBeforeDeath = 5f;

    //these are used if we want the "cooldown" of the heartbeat to go down or up faster, i imagine these will be used with the enemies?
    private float agitationIncrement = 1f;
    private float agitationDecrement = 1f;
    private AudioSource heartBeatSound;

    void Start()
    {
        heartBeatSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Q)){
            print("eyes closed....Heart Rate: " + heartRate);
            heartRate = heartRate + (Time.deltaTime * agitationIncrement);

            
            heartBeatSound.pitch = RemapRange(heartRate,0f,timeBeforeDeath,.5f,3f);
            
        }
        else{
            if(heartRate >= 0){
                heartRate = heartRate - (Time.deltaTime * agitationDecrement);
                print("eyes open....Heart Rate: " + heartRate);
                heartBeatSound.pitch = RemapRange(heartRate,0f,timeBeforeDeath,.5f,3f);
            }
            else{
                heartBeatSound.pitch = 0f;
            }
        }

        if (Input.GetKey(KeyCode.E)){
            
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
