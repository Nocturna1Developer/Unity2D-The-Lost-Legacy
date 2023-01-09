using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ScoreTextScript : MonoBehaviour
{
    Text text;
    public static int coinAmount;
    //public static int coinTarget = 100;

    public void Start() 
    {
        text = GetComponent<Text> ();
        if (ScoreTextScript.coinAmount >= 200)
        {
            gameWon();
        }
    }
    
    public void Update ()
    {
        text.text = coinAmount.ToString(); 
    }

    public void gameWon()
    {
        SceneManager.LoadScene(2); // My "You Win" scren
    }
}
