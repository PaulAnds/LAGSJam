using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Won Games")]
    public bool hasWonMarble;
    public bool hasWonSoccer;
    public bool hasWonDarts;
    
    [Header("Canica Settings")]
    public float numberOfTriesMarble;
    
    [Header("Canica Settings")]
    public float numberOfBalloonsToWin;

    [Header("References")]
    public Text totalPointsMarbleText;
    public Text numberOfTriesMarbleText;

    [HideInInspector] 
    public float numberOfCurrentTries;
    [HideInInspector] 
    public float totalPointsMarble;

    void Update()
    {
        totalPointsMarbleText.text = totalPointsMarble.ToString();
    }

    public void ChangeTextMarbleLeft(float numberOfTriesLeft){
        numberOfTriesMarbleText.text = "Canicas Restantes: " + numberOfTriesLeft.ToString();
    }
}
