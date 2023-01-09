using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticles : MonoBehaviour
{
    private void Start()
    {
        Debug.Log("OBJECT HAS SPAWNED");
        Destroy(gameObject, 2f);
    }
}
