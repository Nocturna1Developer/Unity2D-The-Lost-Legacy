using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CrystalsScript : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private int crystalAmount = 20;
    [SerializeField] private int maxScore = 150;

    void Start () 
     {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        ScoreTextScript.coinAmount += crystalAmount;
        Destroy(gameObject);
        audioSource.Play();
        if(ScoreTextScript.coinAmount >= maxScore)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            ScoreTextScript.coinAmount = 0;
        }
    }
}
