using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Won Games")]
    public bool hasWonCanicas;
    public bool hasWonFootball;
    public bool hasWonDardos;
    
    [Header("Canica Settings")]
    public float numberOfTriesCanicas;
    
    [Header("Canica Settings")]
    public float numberOfBalloonsToWin;

    [Header("References")]
    public Text totalPointsCanicasText;
    public Text numberOfTriesCanicasText;

    [HideInInspector] 
    public float totalPointsCanicas;

    void Update()
    {
        totalPointsCanicasText.text = totalPointsCanicas.ToString();
    }

    public void ChangeTextCanicasLeft(float numberOfTriesLeft){
        numberOfTriesCanicasText.text = "Canicas Restantes: " + numberOfTriesLeft.ToString();
    }
}
