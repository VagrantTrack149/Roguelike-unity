using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puerta_logic : MonoBehaviour
{
    public bool Abierto=false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Abierto){
            Destroy(gameObject);
        }
    }
}
