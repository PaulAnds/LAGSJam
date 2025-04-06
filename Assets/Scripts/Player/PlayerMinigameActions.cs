using UnityEngine;
using UnityEngine.UI;

public class PlayerMinigameActions : MonoBehaviour
{
    //Marbles
    private PlayerMovement playermovement;
    private Vector3 speedDir;
    private PlayerCamera playercamera;
    private PlayerStats playerActions;
    private GameManager gameManager;
    //Darts
    private GameObject dart;
    private bool lockMouse = true;
    private bool isPulled = false;
    private Vector3 pullPosition;
    private Vector3 originalPosition;
    private Vector3 endPosition;
    private float elapseTime;
    //Soccer
    private bool ySoccerRotation= false;
    private bool soccerStrength= false;
    private bool direction = false;
    private Vector3 shootDirection = new Vector3(.02f,0f,0f);

    //These have references in other scripts, need them public, not needed on inspector[HideInInspector]
    [HideInInspector]
    public GameObject soccerArrow;
    [HideInInspector]
    public GameObject scale;
    [HideInInspector]
    public Vector3 soccerBallOriginalLocation;
    [HideInInspector]
    public GameObject soccerBall;
    [HideInInspector]
    public bool xSoccerRotation = true;
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
        soccerArrow = GameObject.Find("SoccerArrow");
        soccerBall = GameObject.Find("SoccerBall");
        dart = GameObject.Find("Dart");
        scale = marble.transform.GetChild(0).GetChild(0).gameObject;
        scale.SetActive(false);
        rb = marble.GetComponent<Rigidbody>(); 
        playercamera = FindFirstObjectByType<PlayerCamera>();
        soccerBallOriginalLocation = soccerBall.transform.position;
    }

    void Update()
    {
        if(!playermovement.canMove){

            if(Input.GetKeyDown(KeyCode.E) && !playercamera.moveCamera){
                ExitGame();
                gameManager.marbleHints.SetActive(true);
                gameManager.goalNumber = 0;
                for(int i = 0; i < 5; i++){
                    gameManager.soccerGoals[i] = -1;
                }
            }

            //Canica
            if(playerActions.currentGame == PlayerStats.CurrentGame.marble){
                if (Input.GetKey(KeyCode.A)&& marble.transform.localPosition.z >= -0.45f && !charging){
                    marble.transform.position -= speedDir * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.D)&& marble.transform.localPosition.z <= 0.13f && !charging){
                    marble.transform.position += speedDir * Time.deltaTime;
                }
                if (Input.GetKey(KeyCode.Space)){
                    charging = true;
                    scale.SetActive(true);
                    if(scale.transform.localScale.x <= 4f){
                        speedDir.y += Time.deltaTime * 200;
                        scale.transform.localScale += new Vector3(0.1f,0,0) * (Time.deltaTime * 5f);
                    }
                }
                if (Input.GetKeyUp(KeyCode.Space)){
                    gameManager.marbleHints.SetActive(false);
                    rb.constraints = RigidbodyConstraints.None;
                    gameManager.playerAS.clip = gameManager.marbleMove;
                    gameManager.playerAS.Play();
                    //          cambiar esto v dependiendo de la posicion del minijuego
                    rb.AddForce(-marble.transform.right * speedDir.y);
                    scale.SetActive(false);
                    speedDir.y = 0f;
                    scale.transform.localScale = new Vector3(1f,1f,1f);
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

            //soccer
            if(playerActions.currentGame == PlayerStats.CurrentGame.soccer){
                
                if(xSoccerRotation){
                    if(soccerArrow.GetComponent<RectTransform>().eulerAngles.y >= 20 && soccerArrow.GetComponent<RectTransform>().eulerAngles.y <= 335){  
                        direction = !direction;
                    }
                    
                    if(direction){
                        soccerArrow.transform.Rotate(0, -gameManager.arrowSpeed/10f, 0);
                    }
                    else{
                        soccerArrow.transform.Rotate(0, gameManager.arrowSpeed/10f, 0);
                    }
                }

                if(ySoccerRotation){
                    if(soccerArrow.GetComponent<RectTransform>().eulerAngles.z >= 350f){  
                        direction = false;
                        //right
                    }
                    else if(soccerArrow.GetComponent<RectTransform>().eulerAngles.z >= 45f){  
                        direction = true;
                        //left
                    }
                    
                    if(direction){
                        soccerArrow.transform.Rotate(0, 0, -gameManager.arrowSpeed/10f);
                    }
                    else{
                        soccerArrow.transform.Rotate(0, 0, gameManager.arrowSpeed/10f);
                    }
                }
                
                if(soccerStrength){
                    if(soccerArrow.GetComponent<RectTransform>().localScale.x <= .009f){  
                        direction = false;
                        //scale up
                    }
                    else if(soccerArrow.GetComponent<RectTransform>().localScale.x >= .02f){  
                        direction = true;
                        //scale down
                    }
                    if(direction){
                        shootDirection.x -= .01f * Time.deltaTime;
                        soccerArrow.transform.localScale = new Vector3(shootDirection.x,soccerArrow.transform.localScale.y,soccerArrow.transform.localScale.z);
                    }
                    else{
                        shootDirection.x += .01f * Time.deltaTime;
                        soccerArrow.transform.localScale = new Vector3(shootDirection.x,soccerArrow.transform.localScale.y,soccerArrow.transform.localScale.z);
                    }
                }

                if (Input.GetKeyDown(KeyCode.Space)){
                    xSoccerRotation = false;
                    if(!xSoccerRotation && !ySoccerRotation && !soccerStrength){
                        ySoccerRotation = true;
                        shootDirection.y = soccerArrow.GetComponent<RectTransform>().eulerAngles.y;
                        //lock y rotation
                    }
                    else if(ySoccerRotation && !xSoccerRotation){
                        ySoccerRotation = false;
                        soccerStrength = true;
                        shootDirection.z = soccerArrow.GetComponent<RectTransform>().eulerAngles.z;
                        //lock z rotation
                    }
                    else if(soccerStrength)
                    {
                        soccerStrength = false;
                        //lock strength -> done this on above if
                        //hacer flecha invisible
                        soccerArrow.GetComponentInChildren<Image>().enabled = false;
                        //shoot
                        gameManager.playerAS.clip = gameManager.soccerHit;
                        gameManager.playerAS.Play();
                        soccerBall.GetComponent<Rigidbody>().AddForce(soccerArrow.transform.right * shootDirection.x * 30000);
                    }
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
