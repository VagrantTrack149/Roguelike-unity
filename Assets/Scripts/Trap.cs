using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    public GameObject Player;
    public GameObject respawn;
    public PlayerMovement PlayerMovement;
    void OnTriggerEnter(Collider other){
        Debug.Log("Hola");
        if (other.CompareTag("Player")){
            PlayerMovement.muerte=true;
            Player.transform.position = respawn.transform.position;
            Debug.Log("Respawn"+ respawn.transform.position);
            Debug.Log("Player"+ Player.transform.position);
            
            Debug.Log("Jugador respawneado en: " + respawn.transform.position);
            Rigidbody rb = Player.GetComponent<Rigidbody>();
            if (rb != null) {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

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
