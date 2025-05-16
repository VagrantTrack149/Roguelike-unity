using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ataque_Aux : MonoBehaviour
{
    public float count;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        count+=Time.deltaTime;
        if (count>=1){
            Destroy(gameObject);
        }
    }
}
