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

    [Header("Soccer Settings")]
    public float arrowSpeed = 4f;

    [Header("References")]
    public Text totalPointsMarbleText;
    public GameObject numberOfTriesMarbleGO;
    public GameObject marbleHints;

    [HideInInspector] 
    public float numberOfCurrentTries;
    [HideInInspector] 
    public float totalPointsMarble;

    void Start()
    {
        numberOfTriesMarbleGO = GameObject.Find("NumberMarblesLeft");
    }
    void Update()
    {
        totalPointsMarbleText.text = totalPointsMarble.ToString();
    }

    public void ChangeTextMarbleLeft(float numberOfTriesLeft){
        for(int i = 0; i < 4; i++){
            numberOfTriesMarbleGO.transform.GetChild(i).gameObject.SetActive(false);
        }
        for(int i = 0; i < numberOfTriesLeft-1; i++){
            numberOfTriesMarbleGO.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
