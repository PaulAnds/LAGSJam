using UnityEngine;

public class PlayerMinigameActions : MonoBehaviour
{
    private PlayerMovement playermovement;
    private GameObject canica;
    private Vector3 speedDir;
    private Rigidbody rb;
    private PlayerCamera playercamera;

    //These have references in other scripts, need them public 
    [HideInInspector]
    public bool charging;

    void Start()
    {
        playermovement = GetComponent<PlayerMovement>();
        speedDir.z = 2f;
        canica = GameObject.Find("Canica");
        rb = canica.GetComponent<Rigidbody>(); 
        playercamera = FindFirstObjectByType<PlayerCamera>();    
    }

    void Update()
    {
        if(!playermovement.canMove){
            if (Input.GetKey(KeyCode.A)&& canica.transform.position.z >= -0.5f && !charging){
                canica.transform.position -= speedDir * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.D)&& canica.transform.position.z <= 0.5f && !charging){
                canica.transform.position += speedDir * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.Space)){
                charging = true;
                speedDir.y += Time.deltaTime * 200;
            }
            if (Input.GetKeyUp(KeyCode.Space)){
                rb.constraints = RigidbodyConstraints.None;
                //          cambiar esto v dependiendo de la posicion del minijuego
                rb.AddForce(-canica.transform.right * speedDir.y);
                speedDir.y = 0f;
            }
            if(Input.GetKeyDown(KeyCode.E) && !playercamera.moveCamera){
                playercamera.positionToMove = playercamera.oldCameraLocation;
                playermovement.canMove = true;
                playercamera.moveCamera = true;
            }
        }
    }

}
