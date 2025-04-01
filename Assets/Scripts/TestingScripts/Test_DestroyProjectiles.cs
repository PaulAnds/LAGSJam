using UnityEngine;

public class Test_DestroyProjectiles : MonoBehaviour
{
    public GameObject sphere;
    public Vector3 spherePosition;
    public PlayerMinigameActions playerActionRef;
    public Rigidbody rb;

    void Start()
    {
        playerActionRef = FindAnyObjectByType<PlayerMinigameActions>();
        spherePosition = sphere.transform.position; 
        rb = sphere.GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Projectile")
        {
            sphere.transform.position = spherePosition;
            sphere.transform.rotation = new Quaternion(0,0,0,0);
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            rb.constraints = RigidbodyConstraints.FreezePositionX;
            playerActionRef.charging = false;
        }
    }
}
