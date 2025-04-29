using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject respawn;
    public PlayerMovement PlayerMovement;
    
    void OnTriggerEnter(Collider other){
        if (other.CompareTag("Player")){
            PlayerMovement.muerte=true;
            Debug.Log("Entrar");
        }
        if (other.CompareTag("Enemy")){
            Destroy(other.gameObject);  
        }
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
