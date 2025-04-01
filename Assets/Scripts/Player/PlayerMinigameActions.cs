using UnityEngine;

public class PlayerMinigameActions : MonoBehaviour
{
    public PlayerMovement playermovement;
    public GameObject canica;
    public Vector3 speedDir;
    public bool charging;
    public Rigidbody rb;

    void Start()
    {
        playermovement = GetComponent<PlayerMovement>();
        speedDir.z = 2f;
        rb = canica.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(!playermovement.canMove){
            if (Input.GetKey(KeyCode.LeftArrow)&& canica.transform.position.z >= -0.5f && !charging){
                canica.transform.position -= speedDir * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.RightArrow)&& canica.transform.position.z <= 0.5f && !charging){
                canica.transform.position += speedDir * Time.deltaTime;
            }
            if (Input.GetKey(KeyCode.UpArrow)){
                charging = true;
                speedDir.y += Time.deltaTime * 200;
            }
            if (Input.GetKeyUp(KeyCode.UpArrow)){
                rb.constraints = RigidbodyConstraints.None;
                rb.AddForce(transform.forward * speedDir.y);
                speedDir.y = 0f;
            }
        }
    }
}
