using UnityEngine;

public class PlayerMinigameActions : MonoBehaviour
{
    public PlayerMovement playermovement;
    public GameObject canica;
    public Vector3 speedDir;
    public bool charging;
    public Rigidbody rb;
    public PlayerCamera playercamera;


    void Start()
    {
        playermovement = GetComponent<PlayerMovement>();
        speedDir.z = 2f;
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
