using UnityEngine;
using UnityEngine.UI;

public class Test_DestroyBall : MonoBehaviour
{

    public PlayerMinigameActions playerActionsRef;
    public GameObject soccerBallPrefab;
    public GameObject soccerBallParent;


    void Start()
    {
        playerActionsRef = FindAnyObjectByType<PlayerMinigameActions>();
    }
    void OnCollisionEnter(Collision collision)
    {
        
        if(collision.gameObject.tag == "Projectile")
        {
            Destroy(collision.gameObject,2);
            playerActionsRef.xSoccerRotation = true;
            //2seg - spawnear siguiente bola 
            Invoke("RespawnBall",2.3f);
        }
    }


    public void RespawnBall(){
        if(!playerActionsRef.soccerBall){
            playerActionsRef.soccerArrow.GetComponentInChildren<Image>().enabled = true;
            playerActionsRef.soccerArrow.GetComponentInChildren<RectTransform>().transform.rotation = new Quaternion(0,0,0,0);
            playerActionsRef.soccerArrow.GetComponentInChildren<RectTransform>().transform.localScale = new Vector3(.02f, .004f, .006f);
            playerActionsRef.soccerBall = Instantiate(soccerBallPrefab,playerActionsRef.soccerBallOriginalLocation,new Quaternion(0f,0f,0f,0f));
            playerActionsRef.soccerBall.transform.SetParent(soccerBallParent.transform);
        }
    }

}
