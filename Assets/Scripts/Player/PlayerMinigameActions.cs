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

    //dardo variables
    public GameObject dardo;
    public bool lockMouse = true;
    public bool isPulled = false;
    public bool choosingDartPosition = true;
    public Vector3 pullPosition;
    public Vector3 originalPosition;
    public Vector3 endPosition;
    public float elapseTime;
    public bool shootingDart;
    public float frequency = 50f;
    public float speed = 3f;

    void Start()
    {
        playermovement = GetComponent<PlayerMovement>();
        playerActions = GetComponent<PlayerStats>();
        speedDir.z = canicaSpeed;
        canica = GameObject.Find("Canica");
        dardo = GameObject.Find("Dardo");
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
                
                if(choosingDartPosition){
                    Vector3 mousePos = Input.mousePosition;
                    mousePos.x = RemapRange(mousePos.x,0f,563f,-7f,-5f);
                    mousePos.y = RemapRange(mousePos.y,317f,1f,2.75f,1.37f);
                    mousePos.z = -6.5f; // Keep the object in the 2D plane
                    dardo.transform.position = mousePos;
                    Vector3 p = dardo.transform.position;
                    p.y = (Mathf.Cos(Time.time*speed)/frequency);
                    p.x = (Mathf.Sin(Time.time*speed)/frequency);
                    dardo.transform.position = new Vector3(mousePos.x-p.x,mousePos.y+p.y,mousePos.z);
                }
                
                if(lockMouse){
                    Cursor.lockState = CursorLockMode.Confined;
                    lockMouse = false;
                }
                if (Input.GetKey(KeyCode.Mouse0)){
                    if(!isPulled){
                        originalPosition = dardo.transform.position ;
                        endPosition = originalPosition + new Vector3(0,0,1.5f);
                        pullPosition = originalPosition - new Vector3(0,0,.25f) ;
                        isPulled = true;
                    }
                    elapseTime += Time.deltaTime;
                    float percentageComplete = elapseTime/2;
                    choosingDartPosition = false;
                    //stop animation of moving and start animation of pulling back
                    dardo.transform.position = Vector3.Lerp(originalPosition, pullPosition, percentageComplete);
                }
                if (Input.GetKeyUp(KeyCode.Mouse0)){
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
                    dardo.transform.position = Vector3.Lerp(pullPosition, endPosition, percentageComplete);
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
