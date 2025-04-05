using UnityEngine;

public class PlayerMinigameActions : MonoBehaviour
{
    //Marbles
    private PlayerMovement playermovement;
    private Vector3 speedDir;
    private PlayerCamera playercamera;
    private PlayerStats playerActions;
    private GameManager gameManager;
    private GameObject scale;
    //Darts
    private GameObject dart;
    private bool lockMouse = true;
    private bool isPulled = false;
    private Vector3 pullPosition;
    private Vector3 originalPosition;
    private Vector3 endPosition;
    private float elapseTime;


    //These have references in other scripts, need them public, not needed on inspector
    [HideInInspector]
    public bool charging;
    [HideInInspector]
    public GameObject marble;
    [HideInInspector]
    public Rigidbody rb;
    [HideInInspector]
    public bool shootingDart;
    [HideInInspector]
    public bool choosingDartPosition = true;

    void Start()
    {
        playermovement = GetComponent<PlayerMovement>();
        gameManager = FindFirstObjectByType<GameManager>();
        playerActions = GetComponent<PlayerStats>();
        speedDir.z = gameManager.marbleSpeed;
        marble = GameObject.Find("Marble");
        dart = GameObject.Find("Dart");
        scale = GameObject.Find("Scale");
        rb = marble.GetComponent<Rigidbody>(); 
        playercamera = FindFirstObjectByType<PlayerCamera>();
    }

    void Update()
    {
        if(!playermovement.canMove){

            if(Input.GetKeyDown(KeyCode.E) && !playercamera.moveCamera){
                ExitGame();
            }

            //Canica
            if(playerActions.currentGame == PlayerStats.CurrentGame.marble){
                if (Input.GetKey(KeyCode.A)&& marble.transform.localPosition.z >= 0.1f && !charging){
                    marble.transform.position -= speedDir * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.D)&& marble.transform.localPosition.z <= 0.98f && !charging){
                    marble.transform.position += speedDir * Time.deltaTime;
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
                    rb.AddForce(-marble.transform.right * speedDir.y);
                    speedDir.y = 0f;
                    scale.transform.localScale = new Vector3(.1f,.1f,.1f);
                }
            }

            //Darts
            if(playerActions.currentGame == PlayerStats.CurrentGame.dart){
                
                if(choosingDartPosition){
                    Vector3 mousePos = Input.mousePosition;
                    mousePos.x = RemapRange(mousePos.x,0f,Display.main.systemWidth,1f,-1f);
                    mousePos.y = RemapRange(mousePos.y,Display.main.systemHeight,0f,2.7f,1.35f);
                    mousePos.z = .8f; // Keep the object in the 2D plane
                    dart.transform.localPosition = mousePos;
                    Vector3 p = dart.transform.localPosition;
                    p.y = (Mathf.Cos(Time.time*gameManager.speed)/gameManager.frequency);
                    p.x = (Mathf.Sin(Time.time*gameManager.speed)/gameManager.frequency);
                    dart.transform.localPosition = new Vector3(mousePos.x-p.x,mousePos.y+p.y,mousePos.z);
                }
                
                if(lockMouse){
                    Cursor.lockState = CursorLockMode.Confined;
                    lockMouse = false;
                }
                if (Input.GetKey(KeyCode.Mouse0) && !shootingDart){
                    if(!isPulled){
                        originalPosition = dart.transform.position;
                        endPosition = originalPosition + new Vector3(0,0,1.5f);
                        pullPosition = originalPosition - new Vector3(0,0,.25f);
                        isPulled = true;
                    }
                    elapseTime += Time.deltaTime;
                    float percentageComplete = elapseTime/2;
                    choosingDartPosition = false;
                    //stop animation of moving and start animation of pulling back
                    dart.transform.position = Vector3.Lerp(originalPosition, pullPosition, percentageComplete);
                }
                if (Input.GetKeyUp(KeyCode.Mouse0) && !shootingDart){
                    //shoot
                    if(isPulled){
                        elapseTime = 0f;
                    }
                    isPulled = false;
                    shootingDart = true;
                }

                if(shootingDart){
                    elapseTime += Time.deltaTime;
                    float percentageComplete = elapseTime/.16f;
                    dart.transform.position = Vector3.Lerp(pullPosition, endPosition, percentageComplete);
                }
            }
        }
    }

    public void ExitGame(){
        playercamera.positionToMove = playercamera.oldCameraLocation;
        playermovement.canMove = true;
        playercamera.moveCamera = true;
        playerActions.currentGame = PlayerStats.CurrentGame.none;
    }

    private float RemapRange(float value, float InputA, float InputB, float OutputA, float OutputB)
    {
        return (value - InputA) / (InputB - InputA) * (OutputB - OutputA) + OutputA;
    }

}
