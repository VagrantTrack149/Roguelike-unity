using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta_logic : MonoBehaviour
{
    public bool Abierto=false;

    void Update()
    {
        if (Abierto){
            Destroy(gameObject);
        }
    }
}
