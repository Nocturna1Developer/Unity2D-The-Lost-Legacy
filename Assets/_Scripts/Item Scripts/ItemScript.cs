using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour {

    public float hook_Speed;

    public int scoreValue;

    void OnDisable() {  
        GameplayManager.instance.DisplayScore(scoreValue);
    }

}
