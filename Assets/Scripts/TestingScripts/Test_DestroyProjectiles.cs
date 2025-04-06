using UnityEngine;

public class Test_DestroyProjectiles : MonoBehaviour
{
    private PlayerMinigameActions playerActionRef;
    [HideInInspector]
    public Rigidbody rb;
    [HideInInspector]
    public Vector3 spherePosition;
    [HideInInspector]
    public GameObject marble;
    public GameManager gameManager;

    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        playerActionRef = FindAnyObjectByType<PlayerMinigameActions>();
        marble = GameObject.Find("Marble");
        spherePosition = marble.transform.position; 
        rb = marble.GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.tag == "Projectile")
        {
            gameManager.playerAS.Stop();
            marble.transform.position = spherePosition;
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            rb.constraints = RigidbodyConstraints.FreezePositionX;
            playerActionRef.charging = false;
            rb.freezeRotation = true;
            marble.transform.rotation = new Quaternion(0,0,0,0);
        }
    }
}
