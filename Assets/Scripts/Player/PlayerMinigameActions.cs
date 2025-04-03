using UnityEngine;

public class PlayerMinigameActions : MonoBehaviour
{
    private PlayerMovement playermovement;
    [HideInInspector]
    public GameObject canica;
    private Vector3 speedDir;
    [HideInInspector]
    public Rigidbody rb;
    private PlayerCamera playercamera;
    private PlayerStats playerActions;
    private GameObject scale;
    public float canicaSpeed;

    //These have references in other scripts, need them public 
    [HideInInspector]
    public bool charging;

    void Start()
    {
        playermovement = GetComponent<PlayerMovement>();
        playerActions = GetComponent<PlayerStats>();
        speedDir.z = canicaSpeed;
        canica = GameObject.Find("Canica");
        scale = GameObject.Find("Scale");
        rb = canica.GetComponent<Rigidbody>(); 
        playercamera = FindFirstObjectByType<PlayerCamera>();    
    }

    void Update()
    {
        if(!playermovement.canMove){

            if(Input.GetKeyDown(KeyCode.E) && !playercamera.moveCamera){
                ExitGame();
            }

            //Canica
            if(playerActions.currentGame == PlayerStats.CurrentGame.canica){
                if (Input.GetKey(KeyCode.A)&& canica.transform.localPosition.z >= 0.1f && !charging){
                    canica.transform.position -= speedDir * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.D)&& canica.transform.localPosition.z <= 0.98f && !charging){
                    canica.transform.position += speedDir * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.Space)){
                    charging = true;
                    if(scale.transform.localScale.x <= 0.7f){
                        speedDir.y += Time.deltaTime * 200;
                        scale.transform.localScale += new Vector3(0.1f,0,0) * (Time.deltaTime * 2.3f);
                    }
                }
                if (Input.GetKeyUp(KeyCode.Space)){
                    rb.constraints = RigidbodyConstraints.None;
                    //          cambiar esto v dependiendo de la posicion del minijuego
                    rb.AddForce(-canica.transform.right * speedDir.y);
                    speedDir.y = 0f;
                    scale.transform.localScale = new Vector3(.1f,.1f,.1f);
                }
            }

            //Darts
            if(playerActions.currentGame == PlayerStats.CurrentGame.dardo){
            
            }
        }
    }

    public void ExitGame(){
        playercamera.positionToMove = playercamera.oldCameraLocation;
        playermovement.canMove = true;
        playercamera.moveCamera = true;
        playerActions.currentGame = PlayerStats.CurrentGame.none;
    }

}
