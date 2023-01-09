using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FungusScript : MonoBehaviour
{
    AudioSource audioSource;

    [SerializeField] private int fungusAmount = 20;
    [SerializeField] private GameObject explosionPrefab;

     void Start () 
     {
        audioSource = GetComponent<AudioSource>();
     }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        ScoreTextScript.coinAmount -= fungusAmount;
        if (collision.gameObject.tag == "Player") 
        {  
            if (explosionPrefab)
            {	
                Instantiate(explosionPrefab, transform.position, transform.rotation);
            }
        }
        Destroy(gameObject); 
    }
}
