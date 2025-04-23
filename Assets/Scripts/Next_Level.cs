using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Next_Level : MonoBehaviour
{
    public PlayerMovement PlayerMovement;
    // Start is called before the first frame update
    void OnTriggerEnter(Collider other) {
        PlayerMovement.muerte=true;
    }
}
