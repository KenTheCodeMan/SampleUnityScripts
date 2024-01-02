using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    private GameManager gameManager;
    private Button button;
    public int difficulty;
  
    void Start()
    {
        button = GetComponent<Button>(); //gets button
        button.onClick.AddListener(SetDifficulty); 
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void SetDifficulty()
    {
        gameManager.StartGame(difficulty); //passes difficulty number into StartGame();
    }
}
