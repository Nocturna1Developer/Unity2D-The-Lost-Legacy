using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "GameController")
        {
            Destroy(gameObject, 0.2f);
        }
    }
}
