using UnityEngine;

public class Test_DestroyProjectiles : MonoBehaviour
{
    private GameObject canica;
    private Vector3 spherePosition;
    private PlayerMinigameActions playerActionRef;
    private Rigidbody rb;

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
            canica.transform.rotation = new Quaternion(0,0,0,0);
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.constraints = RigidbodyConstraints.FreezePositionY;
            rb.constraints = RigidbodyConstraints.FreezePositionX;
            playerActionRef.charging = false;
        }
    }
}
