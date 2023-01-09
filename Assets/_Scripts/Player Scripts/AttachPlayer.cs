using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttachPlayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "GameController")
        {
            other.transform.parent = this.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "GameController")
        {
            other.transform.parent = null;
        }
    }
}