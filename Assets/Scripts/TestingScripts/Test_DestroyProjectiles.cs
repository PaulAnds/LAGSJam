using UnityEngine;

public class Test_DestroyProjectiles : MonoBehaviour
{
    [HideInInspector]
    public GameObject canica;
    [HideInInspector]
    public Vector3 spherePosition;
    private PlayerMinigameActions playerActionRef;
    [HideInInspector]
    public Rigidbody rb;

    void Start()
    {
        playerActionRef = FindAnyObjectByType<PlayerMinigameActions>();
        canica = GameObject.Find("Canica");
        spherePosition = canica.transform.position; 
        rb = canica.GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.tag == "Projectile")
        {
            canica.transform.position = spherePosition;
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            rb.constraints = RigidbodyConstraints.FreezePositionX;
            playerActionRef.charging = false;
            rb.freezeRotation = true;
            canica.transform.rotation = new Quaternion(0,0,0,0);
        }
    }
}
