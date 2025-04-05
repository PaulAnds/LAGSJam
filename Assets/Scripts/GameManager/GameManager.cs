using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Won Games")]
    public bool hasWonMarble;
    public bool hasWonSoccer;
    public bool hasWonDarts;
    
    [Header("Marble Settings")]
    public float numberOfTriesMarble;
    public float marbleSpeed = 1.5f;
    
    [Header("Dart Settings")]
    public float numberOfBalloonsToWin;
    public float frequency = 3f;
    public float speed = 3f;

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
