using UnityEngine;

public class Test_CheckGoal : MonoBehaviour
{
    public bool isTrue;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Projectile"){
            if(isTrue){
                print("yes");
            }
            else{
                print("no");
            }

        }
    }
}
