using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class RayOBJ : MonoBehaviour
{
    public float speed = 20;
    public float count;
    public GameObject player;
    public PlayerMovement rm;
    public GameObject enemigo;
    public EnemigoIA ia;
    public GameObject modelo;
    public Rigidbody rb;
    public NavMeshAgent agent;
    public RayController ray;

    // Start is called before the first frame update
    void Start()
    {
        player=GameObject.Find("Player");
        rm=player.GetComponent<PlayerMovement>();
        modelo=GameObject.Find("Model");
        ray=player.GetComponent<RayController>();
    }

    // Update is called once per frame
    void Update()
    {
        count+=Time.deltaTime;
        transform.Translate(Vector3.forward*speed*Time.deltaTime);
        if (count>=2){
            Destroy(gameObject);
            count=0;
        }
    }
    /*void OnTriggerEnter(Collider other) {
        //Debug.Log("Destruir");
        if (other.CompareTag("Enemy")){
            PlayerMovement.muerte=true; 
            Debug.Log("Muerte");
        }
        Destroy(gameObject);
    }*/
    void OnCollisionEnter(Collision other) {
        
         if (other.gameObject.CompareTag("Enemy")){
            Debug.Log("Muerte");
            //rm.HandleRespawn();
            player.transform.position=other.transform.position;
            enemigo=other.gameObject;
            menso();
            Destroy(gameObject);
        }
    }
    public void menso(){
        if (enemigo!=null){
            enemigo.tag="Poseido";
            ia=enemigo.GetComponent<EnemigoIA>();
            rb=enemigo.GetComponent<Rigidbody>();
            agent=enemigo.GetComponent<NavMeshAgent>();
            if (rb != null){
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
            Destroy(rb);
            agent.enabled = false;
            ia.enabled=false;
            modelo.SetActive(false);
            player.transform.position=enemigo.transform.position;
            enemigo.transform.rotation=player.transform.rotation;
            enemigo.transform.parent=player.transform;
            player.tag="Enemy"; 
        }
        ray.trans=true;
    }
    
}
